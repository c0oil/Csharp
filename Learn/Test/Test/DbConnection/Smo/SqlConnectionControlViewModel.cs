using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CodeFirst;
using Test.ViewModel;

namespace Test.DbConnection.Smo
{
    public class SqlConnectionControlViewModel: ViewModelBase
    {
        private static readonly SqlConnectionStringBuilder DefaultValue = new SqlConnectionStringBuilder { IntegratedSecurity = true, MultipleActiveResultSets = true };

        private readonly AsyncSmoTasks smoTasks;
        private bool needUpdateDatabases = true;

        public SqlConnectionControlViewModel() : this(new AsyncSmoTasks())
        {
        }

        public SqlConnectionControlViewModel(AsyncSmoTasks smoTasks)
        { 
            this.smoTasks = smoTasks;
            LoadServersAsync();
        }

        private ICommand deleteDbCommand;
        public ICommand DeleteDbCommand
        {
            get { return GetDelegateCommand<object>(ref deleteDbCommand, x => DeleteDb()); }
        }

        private ICommand createDbCommand;
        public ICommand CreateDbCommand
        {
            get { return GetDelegateCommand<object>(ref createDbCommand, x => CreateDb()); }
        }

        public string Header
        {
            get { return "Sql Configuration"; }
        }

        private readonly ObservableCollection<string> databases = new ObservableCollection<string>();
        public ObservableCollection<string> Databases
        {
            get { return databases; }
        }
        
        private readonly ObservableCollection<string> servers = new ObservableCollection<string>();
        public ObservableCollection<string> Servers
        {
            get { return servers; }
        }

        public string Server
        {
            get { return ConnectionBuilder.DataSource; }
            set
            {
                if (ConnectionBuilder.DataSource == value)
                {
                    return;
                }
                ConnectionBuilder.DataSource = value;
                needUpdateDatabases = true;
                OnPropertyChanged(() => Server);
            }
        }

        public string Database
        {
            get { return ConnectionBuilder.InitialCatalog; }
            set
            {
                ConnectionBuilder.InitialCatalog = value;
                OnPropertyChanged(() => Database);
            }
        }
        
        public bool IntegratedSecurity
        {
            get { return ConnectionBuilder.IntegratedSecurity; }
            set
            {
                ConnectionBuilder.IntegratedSecurity = value;
                OnPropertyChanged(() => IntegratedSecurity);
            }
        }

        public string UserName
        {
            get { return ConnectionBuilder.UserID; }
            set
            {
                ConnectionBuilder.UserID = value;
                OnPropertyChanged(() => UserName);
            }
        }

        public string Password
        {
            get { return ConnectionBuilder.Password; }
            set
            {
                ConnectionBuilder.Password = value;
                OnPropertyChanged(() => Password);
            }
        }

        private SqlConnectionStringBuilder connectionBuilder = DefaultValue;
        public SqlConnectionStringBuilder ConnectionBuilder
        {
            get { return connectionBuilder; }
            set
            {
                connectionBuilder = value ?? DefaultValue;
                needUpdateDatabases = true;
                OnPropertyChanged(() => ConnectionBuilder);

                OnPropertyChanged(() => IntegratedSecurity);
                OnPropertyChanged(() => Password);
                OnPropertyChanged(() => UserName);
                OnPropertyChanged(() => Server);
                OnPropertyChanged(() => Database);
            }
        }

        private bool serversLoading;
        public bool ServersLoading
        {
            get { return serversLoading; }
            private set
            {
                serversLoading = value;
                OnPropertyChanged(() => ServersLoading);
            }
        }

        private bool databasesLoading;
        public bool DatabasesLoading
        {
            get { return databasesLoading; }
            private set
            {
                databasesLoading = value;
                OnPropertyChanged(() => DatabasesLoading);
            }
        }

        public void LoadDatabases()
        {
            if (!needUpdateDatabases)
            {
                return;
            }
            SqlConnectionStringBuilder connString = ConnectionBuilder;
            if (connString == null || string.IsNullOrEmpty(connString.DataSource))
            {
                return;
            }

            Databases.Clear();
            foreach (var database in smoTasks.GetDatabases(ConnectionBuilder).OrderBy(d => d))
            {
                Databases.Add(database);
            }
            needUpdateDatabases = false;
        }

        public async void LoadDatabasesAsync()
        {
            if (!needUpdateDatabases)
            {
                return;
            }

            SqlConnectionStringBuilder connString = ConnectionBuilder;
            if (connString == null || string.IsNullOrEmpty(connString.DataSource))
            {
                return;
            }

            needUpdateDatabases = false;
            DatabasesLoading = true;

            List<string> serverDatabases = await smoTasks.GetDatabasesAsync(connString);
            Databases.Clear();
            foreach (var database in serverDatabases.OrderBy(d => d))
            {
                Databases.Add(database);
            }

            DatabasesLoading = false;
        }

        public async void LoadServersAsync()
        {
            if (Servers.Count > 0)
            {
                return;
            }
            ServersLoading = true;

            IEnumerable<string> sqlServers = await smoTasks.SqlServersAsync;
            Servers.Clear();
            foreach (var server in sqlServers.OrderBy(r => r))
            {
                Servers.Add(server);
            }

            ServersLoading = false;
        }

        public bool TestConnection()
        {
            return TestConnection(ConnectionBuilder.ConnectionString);
        }

        public static bool TestConnection(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.ToString(), "Test connection", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
        }

        private const string DefaultDbName = "Default";
        private string GetNewDbName(string name = null)
        {
            string newDbName = string.IsNullOrWhiteSpace(name) ? DefaultDbName : name;
            string result = newDbName;
            int num = 1;
            while (Databases.Contains(result))
            {
                result = string.Format("{0}({1})", newDbName, num);
                num++;
            }
            return result;
        }

        public void DeleteDb()
        {
            try
            {
                using (var context = new ClientContext(ConnectionBuilder.ConnectionString))
                {
                    context.Database.Delete();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Exception stacktrace", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            needUpdateDatabases = true;
        }

        public void CreateDb()
        {
            var copy = new SqlConnectionStringBuilder(ConnectionBuilder.ConnectionString);
            LoadDatabases();
            
            //if (!TestConnection(new SqlConnectionStringBuilder { DataSource = copy.DataSource }.ConnectionString))
            //    return;

            bool needRewrite = false;
            if (!Databases.Contains(Database))
            {
                copy.InitialCatalog = GetNewDbName(Database);
            }
            else
            {
                var isRewrite = MessageBox.Show(string.Format("Do you want rewrite {0} db?", Database),
                    "Database information", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (isRewrite != MessageBoxResult.Yes)
                {
                    return;
                }
                needRewrite = true;
            }

            try
            {
                using (var context = new ClientContext(copy.ConnectionString))
                {
                    if (needRewrite)
                    {
                        context.Database.Delete();
                        context.SaveChanges();
                    }
                    context.Database.Initialize(false);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Exception stacktrace", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ConnectionBuilder = copy;
            needUpdateDatabases = true;
        }
    }
}
