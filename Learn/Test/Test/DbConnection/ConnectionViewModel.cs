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

        private SqlConnectionStringBuilder connectionBuilder;
        public SqlConnectionStringBuilder ConnectionBuilder
        {
            get { return connectionBuilder; }
            set
            {
                connectionBuilder = value;
                if (ConnectionControl != null)
                {
                    ConnectionControl.ViewModel.ConnectionBuilder = value;
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
            ConnectionBuilder = ConnectionControl.ViewModel.ConnectionBuilder;
            CloseView(true);
        }

        private bool IsValidConnection()
        {
            return ConnectionControl.ViewModel.TestConnection();
        }
    }
}
