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
        private static readonly SqlConnectionStringBuilder DefaultValue = new SqlConnectionStringBuilder { IntegratedSecurity = true };

        private readonly AsyncSmoTasks smoTasks;

        public SqlConnectionControlViewModel() : this(new AsyncSmoTasks())
        {
        }

        public SqlConnectionControlViewModel(AsyncSmoTasks smoTasks)
        {
            this.smoTasks = smoTasks;
            Server = @"C0_OIL-ПК\SQLEXPRESS";
            Database = DefaultDbName;
            //LoadServersAsync();
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
            get { return ConnectionString.DataSource; }
            set
            {
                if (ConnectionString.DataSource == value)
                {
                    return;
                }
                ConnectionString.DataSource = value;
                OnPropertyChanged(() => Server);

                //LoadDatabasesAsync(ConnectionString);
            }
        }

        public string Database
        {
            get { return ConnectionString.InitialCatalog; }
            set
            {
                ConnectionString.InitialCatalog = value;
                OnPropertyChanged(() => Database);
            }
        }
        
        public bool IntegratedSecurity
        {
            get { return ConnectionString.IntegratedSecurity; }
            set
            {
                ConnectionString.IntegratedSecurity = value;
                OnPropertyChanged(() => IntegratedSecurity);
            }
        }

        public string UserName
        {
            get { return ConnectionString.UserID; }
            set
            {
                ConnectionString.UserID = value;
                OnPropertyChanged(() => UserName);
            }
        }

        public string Password
        {
            get { return ConnectionString.Password; }
            set
            {
                ConnectionString.Password = value;
                OnPropertyChanged(() => Password);
            }
        }

        private SqlConnectionStringBuilder connectionString = DefaultValue;
        public SqlConnectionStringBuilder ConnectionString
        {
            get { return connectionString; }
            set
            {
                connectionString = value ?? DefaultValue;
                OnPropertyChanged(() => ConnectionString);

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

        private async void LoadDatabasesAsync(SqlConnectionStringBuilder connString)
        {
            if (connString == null || string.IsNullOrEmpty(connString.DataSource))
            {
                return;
            }

            DatabasesLoading = true;

            List<string> serverDatabases = await smoTasks.GetDatabases(connString);
            foreach (var database in serverDatabases.OrderBy(d => d))
            {
                Databases.Add(database);
            }

            DatabasesLoading = false;
        }
        
        private async void LoadServersAsync()
        {
            ServersLoading = true;

            IEnumerable<string> sqlServers = await smoTasks.SqlServers;
            foreach (var server in sqlServers.OrderBy(r => r))
            {
                servers.Add(server);
            }

            ServersLoading = false;
        }
        
        public bool TestConnection()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString.ConnectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        private const string DefaultDbName = "Default";
        private string GetNewDbName(string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return DefaultDbName;
            }
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

        public void CreateDb()
        {
            var copy = new SqlConnectionStringBuilder(ConnectionString.ConnectionString);
            if (!Databases.Contains(Database))
            {
                copy.InitialCatalog = GetNewDbName(Database);
            }

            using (SqlConnection connection = new SqlConnection(copy.ConnectionString))
            {
                try
                {
                    var context = new SampleContext(connection);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Exception stacktrace", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            ConnectionString = copy;
        }
    }
}
