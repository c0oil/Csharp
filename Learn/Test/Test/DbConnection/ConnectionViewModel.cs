using System.Data.SqlClient;
using System.Windows.Input;
using Test.DbConnection.Smo;
using Test.ViewModel;

namespace Test.DbConnection
{
    public class ConnectionViewModel : ViewModelBase
    {
        public SqlConnectionControl ConnectionControl { get; set; }

        private ICommand okCommand;
        public ICommand OkCommand
        {
            get { return GetDelegateCommand<object>(ref okCommand, x => OnOk()); }
        }
        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return GetDelegateCommand<object>(ref cancelCommand, x => OnCancel()); }
        }

        private SqlConnectionStringBuilder connectionString;
        public SqlConnectionStringBuilder ConnectionString
        {
            get { return connectionString; }
            set
            {
                connectionString = value;
                if (ConnectionControl != null)
                {
                    ConnectionControl.ViewModel.ConnectionString = value;
                }
            }
        }

        private void OnCancel()
        {
            CloseView(false);
        }

        private void OnOk()
        {
            if (!IsValidConnection())
            {
                OnCancel();
            }
            ConnectionString = ConnectionControl.ViewModel.ConnectionString;
            CloseView(true);
        }

        private bool IsValidConnection()
        {
            return ConnectionControl.ViewModel.TestConnection();
        }
    }
}
