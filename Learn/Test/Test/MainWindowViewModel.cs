using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Test.DbConnection;
using Test.DbConnection.Smo;
using Test.Table;
using Test.ViewModel;

namespace Test
{
    public class MainWindowViewModel : ViewModelBase
    {
        public SqlConnectionStringBuilder ConnectionBuilder { get; set; }

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
            ConnectionBuilder = new SqlConnectionStringBuilder(ConnectionHelper.GetConnectionStringSettings());
            CheckConnection();
        }

        private async void CheckConnection()
        {
            IsConnecting = true;
            IsConnected = await Task<bool>.Factory.StartNew(() =>
                ConnectionHelper.TestConnection(ConnectionBuilder.ConnectionString));
            IsConnecting = false;

            if (IsConnected)
            {
                ConnectionHelper.SaveConnectionStringSettings(ConnectionBuilder.ConnectionString);
            }
        }

        private void ShowTables()
        {
            if (!IsConnected)
            {
                MessageBox.Show("Please, connect to database", "Show Tables", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
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
    }
}
