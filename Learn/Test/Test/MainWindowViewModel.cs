using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Test.DbConnection;
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

        private void ShowTables()
        {
            bool? result = ShowDialog<TableView>(connectionView =>
            {
                connectionView.ViewModel.ConnectionString = ConnectionString.ConnectionString;
            });
        }

        private void Connect()
        {
            ConnectionViewModel viewModel = null;
            bool? result = ShowDialog<ConnectionView>(connectionView =>
            {
                viewModel = connectionView.ViewModel;
                viewModel.ConnectionString = ConnectionString;
            });

            ConnectionString = viewModel.ConnectionString;
            IsConnected = result == true;
        }

        public SqlConnectionStringBuilder ConnectionString { get; set; }
    }
}
