using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CodeFirst;
using Test.DbConnection.Smo;
using Test.ViewModel;

namespace Test.Table
{
    public class TableViewModel : ViewModelBase
    {
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

        public DataGrid Grid { get; set; }

        private List<Client> dataGrid;
        public List<Client> DataGrid
        {
            get { return dataGrid; }
            set
            {
                dataGrid = value;
                OnPropertyChanged(() => DataGrid);
            }
        }

        public string ConnectionString { get; set; }


        public void Refresh()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    var context = new SampleContext(connection);
                    DataGrid = context.Clients.ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Exception stacktrace", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }

        private void OnOk()
        {
            CloseView(true);
        }

        private void OnCancel()
        {
            CloseView(false);
        }
    }
}
