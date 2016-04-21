using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CodeFirst;
using Test.BaseUI;
using Test.ViewModel;

namespace Test.Table
{
    public class TableViewModel : ViewModelBase
    {
        private SampleRepository sampleRepository;

        private IEnumerable<ClientObj> clientsFromDbAsObj;

        private List<City> cities;
        private List<Disability> disabilities;
        private List<Nationality> nationalities;
        private List<FamilyStatus> familyStatuses;
        private List<Currency> currencies;
        private IEnumerable<Sex> sexes;

        private List<string> cityNames;
        private List<string> disabilityNames;
        private List<string> nationalityNames;
        private List<string> familyStatusNames;
        private List<string> currencyNames;

        #region Default Columns

        private readonly ColumnInfo[] columns =
        {
            GetColumnInfo(x => x.Surname),
            GetColumnInfo(x => x.Name),
            GetColumnInfo(x => x.MiddleName),
            GetColumnInfo(x => x.BirthDate, ColumnType.DateTime),
            GetColumnInfo(x => x.BirthPlace),
            GetColumnInfo(x => x.Sex, ColumnType.ComboBox),

            GetColumnInfo(x => x.HomePhone),
            GetColumnInfo(x => x.MobilePhone),
            GetColumnInfo(x => x.Email, ColumnType.Hyperlink),

            GetColumnInfo(x => x.PassportSeries),
            GetColumnInfo(x => x.PassportNumber),
            GetColumnInfo(x => x.IdentNumber),
            GetColumnInfo(x => x.IssuedBy),
            GetColumnInfo(x => x.IssueDate, ColumnType.DateTime),

            GetColumnInfo(x => x.RegistrationCity, ColumnType.ComboBox),
            GetColumnInfo(x => x.RegistrationAdress),
            GetColumnInfo(x => x.ResidenseCity, ColumnType.ComboBox),
            GetColumnInfo(x => x.ResidenseAdress),

            GetColumnInfo(x => x.Disability, ColumnType.ComboBox),
            GetColumnInfo(x => x.Nationality, ColumnType.ComboBox),
            GetColumnInfo(x => x.FamilyStatus, ColumnType.ComboBox),

            GetColumnInfo(x => x.IsPensioner, ColumnType.CheckBox),
            GetColumnInfo(x => x.IsReservist, ColumnType.CheckBox),

            GetColumnInfo(x => x.MonthlyIncome, ColumnType.Double),
            GetColumnInfo(x => x.Currency, ColumnType.ComboBox),
        };

        private static ColumnInfo GetColumnInfo<T>(Expression<Func<ObservableRow, T>> p, ColumnType columnType = ColumnType.Text)
        {
            return new ColumnInfo(TypeHelper.GetPropertyName(new ObservableRow(), p), columnType);
        }

        #endregion

        #region Commands

        private ICommand addRowCommand;

        public ICommand AddRowCommand
        {
            get { return GetDelegateCommand<object>(ref addRowCommand, x => OnAddRow()); }
        }

        private ICommand updateRowCommand;

        public ICommand UpdateRowCommand
        {
            get { return GetDelegateCommand<object>(ref updateRowCommand, x => OnUpdateRow()); }
        }

        private ICommand deleteRowCommand;

        public ICommand DeleteRowCommand
        {
            get { return GetDelegateCommand<object>(ref deleteRowCommand, x => OnDeleteRow()); }
        }

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

        private ICommand saveChangesCommand;

        public ICommand SaveChangesCommand
        {
            get { return GetDelegateCommand<object>(ref saveChangesCommand, x => OnSaveChanges()); }
        }

        #endregion

        public DataGrid Grid { get; set; }

        private string connectionString;

        public string ConnectionString
        {
            get { return connectionString; }
            set
            {
                connectionString = value;
                if (ConnectionString != null)
                {
                    sampleRepository = new SampleRepository(ConnectionString);
                }
            }
        }

        private ObservableCollection<ObservableRow> dataGrid;
        public ObservableCollection<ObservableRow> DataGrid
        {
            get { return dataGrid; }
            set
            {
                dataGrid = value;
                OnPropertyChanged(() => DataGrid);
            }
        }

        public TableViewModel()
        {
            if (ConnectionString != null)
            {
                sampleRepository = new SampleRepository(ConnectionString);
            }
        }

        public static void ExecuteAndCatchException(Action method)
        {
            try
            {
                if (method != null)
                {
                    method();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Exception stacktrace", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void Refresh()
        {
            RefreshLists();
            clientsFromDbAsObj = sampleRepository.Select<Client>().
                Include(x => x.Passport).
                Include(x => x.Currency).
                Include(x => x.FamilyStatus).
                Include(x => x.Disability).
                Include(x => x.Nationality).
                Include(x => x.Residense).
                Include(x => x.Residense.City).
                Include(x => x.Registration).
                Include(x => x.Registration.City).
                Select(ClientObj.ConvertToObj).AsEnumerable();
            DataGrid = new ObservableCollection<ObservableRow>(clientsFromDbAsObj.Select(ConvertToRow));
        }

        private void RefreshLists()
        {
            sexes = (IEnumerable<Sex>)Enum.GetValues(typeof(Sex));

            cities = sampleRepository.Select<City>().ToList();
            disabilities = sampleRepository.Select<Disability>().ToList();
            nationalities = sampleRepository.Select<Nationality>().ToList();
            familyStatuses = sampleRepository.Select<FamilyStatus>().ToList();
            currencies = sampleRepository.Select<Currency>().ToList();

            cityNames = cities.Select(x => x.Name).ToList();
            disabilityNames = disabilities.Select(x => x.Name).ToList();
            nationalityNames = nationalities.Select(x => x.Name).ToList();
            familyStatusNames = familyStatuses.Select(x => x.Name).ToList();
            currencyNames = currencies.Select(x => x.Name).ToList();
        }

        public void CreateLayaout()
        {
            Grid.Columns.Clear();
            foreach (ColumnInfo columnInfo in columns)
            {
                Grid.Columns.Add(columnInfo.GetColumn());
            }
        }

        public void OnSaveChanges()
        {
        }

        private void OnAddRow()
        {
            var row = ObservableRow.GetEmptyRow(sexes, cityNames, disabilityNames, nationalityNames, familyStatusNames, currencyNames);
            DataGrid.Add(row);
        }

        private void OnUpdateRow()
        {
            if (Grid.SelectedIndex < 0)
            {
                return;
            }

            if (Grid.SelectedIndex >= clientsFromDbAsObj.Count())
            {
                
            }
            else
            {
                ClientObj selectedRow = ObservableRow.ConvertToObj(DataGrid[Grid.SelectedIndex]);
                Client newClient = ClientObj.ConvertToDataSet(selectedRow, sexes, cities, disabilities, nationalities, familyStatuses, currencies);
                Client origClient = sampleRepository.Select<Client>().FirstOrDefault(x => x.ClientId == newClient.ClientId);
                newClient.CopyTo(origClient);
                if (origClient.NotValid())
                {
                    return;
                }
                sampleRepository.Update(origClient);
            }
        }

        private void OnDeleteRow()
        {
        }

        private void OnOk()
        {
            CloseView(true);
        }

        private void OnCancel()
        {
            CloseView(false);
        }

        private ObservableRow ConvertToRow(ClientObj client)
        {
            return ObservableRow.ConvertToRow(client, sexes, cityNames, disabilityNames, nationalityNames, familyStatusNames, currencyNames);
        }
    }
}
