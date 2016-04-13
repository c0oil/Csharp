using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Test.Db;
using Test.ViewModel;

namespace Test
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string findTemplate = "London";
        public string FindTemplate
        {
            get { return findTemplate; }
            set
            {
                findTemplate = value;
                OnPropertyChanged(() => FindTemplate);
            }
        }

        private ICommand refreshCommand;
        public ICommand RefreshCommand
        {
            get { return GetDelegateCommand<object>(ref refreshCommand, x => Refresh()); }
        }

        private ObservableCollection<Row> gridData = new ObservableCollection<Row>();
        public ObservableCollection<Row> GridData
        {
            get { return gridData; }
            set
            {
                gridData = value;
                OnPropertyChanged(() => GridData);
            }
        }

        public void Refresh()
        {
            DataContext db = new DataContext(@"c:\linqtest5\NORTHWND.MDF");
            Table<DbTables.Customer> customers = db.GetTable<DbTables.Customer>();
            IQueryable<DbTables.Customer> custQuery = customers.Where(cust => cust.City.StartsWith(FindTemplate));

            GridData.Clear();
            foreach (DbTables.Customer customer in custQuery)
            {
                GridData.Add(new Row
                {
                    City = customer.City,
                    CustomerID = customer.CustomerID
                });
            }
        }

        public class Row
        {
            public string CustomerID { get; set; }
            public string City { get; set; }
        }

    }
}
