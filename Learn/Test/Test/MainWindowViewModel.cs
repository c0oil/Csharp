using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Test.DbConnection;
using Test.DbConnection.Smo;
using Test.Table;
using Test.ViewModel;

namespace Test
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get { return GetDelegateCommand<object>(ref connectCommand, x => Connect()); }
        }

        private ICommand showTablesCommand;
        public ICommand ShowTablesCommand
        {
            get { return GetDelegateCommand<object>(ref showTablesCommand, x => ShowTables()); }
        }
        
        private bool isConnecting;
        public bool IsConnecting
        {
            get { return isConnecting; }
            set
            {
                isConnecting = value;
                OnPropertyChanged(() => IsConnecting);
            }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                OnPropertyChanged(() => IsConnected);
            }
        }

        public MainWindowViewModel()
        {
            ConnectionBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"C0_OIL-ПК\SQLEXPRESS",
                InitialCatalog = "Default",
                IntegratedSecurity = true,
            };
            CheckConnection();
        }

        private async void CheckConnection()
        {
            IsConnecting = true;
            IsConnected = await Task<bool>.Factory.StartNew(() => 
                SqlConnectionControlViewModel.TestConnection(ConnectionBuilder.ConnectionString));
            IsConnecting = false;
        }

        private void ShowTables()
        {
            bool? result = ShowDialog<TableView>(connectionView =>
            {
                connectionView.ViewModel.ConnectionString = ConnectionBuilder.ConnectionString;
            });
        }

        private void Connect()
        {
            ConnectionViewModel viewModel = null;
            bool? result = ShowDialog<ConnectionView>(connectionView =>
            {
                viewModel = connectionView.ViewModel;
                viewModel.ConnectionBuilder = ConnectionBuilder;
            });
            if (result == true)
            {
                ConnectionBuilder = viewModel.ConnectionBuilder;
            }

            CheckConnection();
        }

        public SqlConnectionStringBuilder ConnectionBuilder { get; set; }
    }
}
