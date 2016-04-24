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
using Test.BaseUI.Columns;
using Test.ViewModel;

namespace Test.Table
{
    public class TableViewModel : ViewModelBase
    {
        private ClientRepository repository;

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
                    repository = new ClientRepository(ConnectionString);
                    CreateLayaout();
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
                repository = new ClientRepository(ConnectionString);
                CreateLayaout();
            }
        }

        public void Refresh()
        {
            IEnumerable<ClientObj> allClients = repository.GetAll();
            DataGrid = new ObservableCollection<ObservableRow>(allClients.Select(ObservableRow.ConvertToRow));
        }

        public void CreateLayaout()
        {
            ColumnInfo[] columns =
            {
                GetColumnInfo(x => x.Surname),
                GetColumnInfo(x => x.Name),
                GetColumnInfo(x => x.MiddleName),
                GetColumnInfo(x => x.BirthDate, ColumnType.DateTime),
                GetColumnInfo(x => x.BirthPlace),
                GetColumnInfo(x => x.Sex, ColumnType.ComboBox, new ObservableCollection<object>(Enum.GetValues(typeof(Sex)).Cast<object>())),

                GetColumnInfo(x => x.HomePhone, ColumnType.MaskedText, stringMask: "0 - 00 - 00"),
                GetColumnInfo(x => x.MobilePhone, ColumnType.MaskedText, stringMask: "(000) 000 - 00 - 00"),
                GetColumnInfo(x => x.Email, ColumnType.Hyperlink),

                GetColumnInfo(x => x.PassportSeries),
                GetColumnInfo(x => x.PassportNumber, ColumnType.MaskedText, stringMask: ">LL0000000"),
                GetColumnInfo(x => x.IdentNumber, ColumnType.MaskedText, stringMask: "AAAAAAAAAAAAAA"),
                GetColumnInfo(x => x.IssuedBy),
                GetColumnInfo(x => x.IssueDate, ColumnType.DateTime),

                GetColumnInfo(x => x.RegistrationCity, ColumnType.ComboBox, repository.GetNames<City>()),
                GetColumnInfo(x => x.RegistrationAdress),
                GetColumnInfo(x => x.ResidenseCity, ColumnType.ComboBox, repository.GetNames<City>()),
                GetColumnInfo(x => x.ResidenseAdress),

                GetColumnInfo(x => x.Disability, ColumnType.ComboBox, repository.GetNames<Disability>()),
                GetColumnInfo(x => x.Nationality, ColumnType.ComboBox, repository.GetNames<Nationality>()),
                GetColumnInfo(x => x.FamilyStatus, ColumnType.ComboBox, repository.GetNames<FamilyStatus>()),

                GetColumnInfo(x => x.IsPensioner, ColumnType.CheckBox),
                GetColumnInfo(x => x.IsReservist, ColumnType.CheckBox),

                GetColumnInfo(x => x.MonthlyIncome, ColumnType.Double),
                GetColumnInfo(x => x.Currency, ColumnType.ComboBox, repository.GetNames<Currency>()),
            };

            Grid.Columns.Clear();
            foreach (ColumnInfo columnInfo in columns)
            {
                Grid.Columns.Add(columnInfo.GetColumn());
            }
        }

        private void OnAddRow()
        {
            var row = ObservableRow.GetEmptyRow();
            DataGrid.Add(row);
        }

        private void OnUpdateRow()
        {
            if (Grid.SelectedIndex < 0)
            {
                return;
            }

            ObservableRow selectedRow = (ObservableRow)Grid.SelectedItem;
            bool rowHasError = Validation.GetHasError(Grid.ItemContainerGenerator.ContainerFromItem(selectedRow));
            if (rowHasError)
            {
                MessageBox.Show("Please, input correct data", "Update Row", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            repository.UpdateOrAdd(ObservableRow.ConvertToObj(selectedRow));
        }

        private void OnDeleteRow()
        {
            if (Grid.SelectedIndex < 0)
            {
                return;
            }

            ObservableRow selectedRow = (ObservableRow)Grid.SelectedItem;
            if (!selectedRow.IsNew)
            {
                repository.Delete(selectedRow.ClientId);
            }
            DataGrid.Remove(selectedRow);
        }

        private void OnOk()
        {
            CloseView(true);
        }

        private void OnCancel()
        {
            CloseView(false);
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
                MessageBox.Show(e.ToString(), "Stacktrace", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private static ColumnInfo GetColumnInfo<T>(Expression<Func<ObservableRow, T>> p, ColumnType columnType = ColumnType.Text,
            IEnumerable<object> itemSource = null, string stringMask = null)
        {
            return new ColumnInfo(TypeHelper.GetPropertyName(new ObservableRow(), p), columnType)
            {
                ItemSource = itemSource,
                InputMask = stringMask,
            };
        }
    }
}
