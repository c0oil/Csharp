using System;
using System.Collections.Generic;
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

        public DataGrid Grid { get; set; }
        public string ConnectionString { get; set; }

        private List<ObservableRow> dataGrid;
        public List<ObservableRow> DataGrid
        {
            get { return dataGrid; }
            set
            {
                dataGrid = value;
                OnPropertyChanged(() => DataGrid);
            }
        }

        public void OnSaveChanges()
        {
            var context = new SampleContext(ConnectionString);
            context.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));
        }

        public void Refresh()
        {
            try
            {
                var context = new SampleContext(ConnectionString);
                context.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));
                IEnumerable<Sex> sexes = (IEnumerable<Sex>) Enum.GetValues(typeof(Sex));
                IEnumerable<string> cities = context.Cities.Select(x => x.CityId).ToList();
                IEnumerable<string> disabilities = context.Disabilities.Select(x => x.DisabilityId).ToList();
                IEnumerable<string> nationalities = context.Nationalities.Select(x => x.NationalityId).ToList();
                IEnumerable<string> familyStatuses = context.FamilyStatuses.Select(x => x.FamilyStatusId).ToList();
                IEnumerable<string> currencies = context.Currencies.Select(x => x.CurrencyId).ToList();
                IEnumerable<ClientObj> clientsObj = context.Clients.
                    Include(x => x.Passport).
                    Include(x => x.Currency).
                    Include(x => x.FamilyStatus).
                    Include(x => x.Disability).
                    Include(x => x.Nationality).
                    Include(x => x.Residense).
                    Include(x => x.Residense.City).
                    Include(x => x.Registration).
                    Include(x => x.Registration.City).
                    Select(ClientObj.ConvertToRow).AsEnumerable();
                DataGrid = clientsObj.Select(x => ConvertToRow(x, sexes, cities, disabilities, nationalities, familyStatuses, currencies)).ToList();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Exception stacktrace", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        public void CreateLayaout()
        {
            Grid.Columns.Clear();
            foreach (ColumnInfo columnInfo in columns)
            {
                Grid.Columns.Add(columnInfo.GetColumn());
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

        private ObservableRow ConvertToRow(ClientObj client,
            IEnumerable<Sex> sexes, IEnumerable<string> cities, IEnumerable<string> disabilities,
            IEnumerable<string> nationalities, IEnumerable<string> familyStatuses, IEnumerable<string> currencies)
        {
            return ObservableRow.ConvertToRow(client, sexes, cities, disabilities, nationalities, familyStatuses, currencies);
        }
    }
}
