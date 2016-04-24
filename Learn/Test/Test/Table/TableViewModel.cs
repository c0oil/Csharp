using System;
using System.Windows.Input;
using CodeFirst;
using Test.ViewModel;

namespace Test.Table
{
    public class TableViewModel : ViewModelBase
    {
        private ClientRepository clientRepository;

        #region Commands

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

        private void OnOk()
        {
            CloseView(true);
        }

        private void OnCancel()
        {
            CloseView(false);
        }

        #endregion

        private string connectionString;
        public string ConnectionString
        {
            get { return connectionString; }
            set
            {
                connectionString = value;
                if (ConnectionString != null)
                {
                    clientRepository = new ClientRepository(ConnectionString);

                    ListsFrame.ViewModel.ClientRepository = clientRepository;
                    ListsFrame.ViewModel.SelectedList = ClientList.City;
                    TableFrame.ViewModel.ClientRepository = clientRepository;
                }
            }
        }

        public ListsFrame ListsFrame { get; set; }
        public TableFrame TableFrame { get; set; }
    }
}
