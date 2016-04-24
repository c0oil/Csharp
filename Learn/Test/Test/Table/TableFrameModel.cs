using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class TableFrameModel : ViewModelBase
    {
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
            ClientRepository.UpdateOrAdd(ObservableRow.ConvertToObj(selectedRow));
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
                ClientRepository.Delete(selectedRow.ClientId);
            }
            DataGrid.Remove(selectedRow);
        }

        #endregion

        public DataGrid Grid { get; set; }
        public ClientRepository ClientRepository { get; set; }

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

        public void Refresh()
        {
            ClientRepository.RefreshLists();
            CreateLayaout();

            var t = ClientRepository.GetTest();
            var p = ClientRepository.Select<City>();
            IEnumerable<ClientObj> allClients = ClientRepository.GetAll();
            DataGrid = new ObservableCollection<ObservableRow>(allClients.Select(ObservableRow.ConvertToRow));
        }

        public void CreateLayaout()
        {
            ColumnInfo[] columns =
            {
                GetColumnInfo(x => x.Surname, caption: "Фамилия"),
                GetColumnInfo(x => x.Name, caption: "Имя"),
                GetColumnInfo(x => x.MiddleName, caption: "Отчество"),
                GetColumnInfo(x => x.BirthDate, ColumnType.DateTime, caption: "Дата рождения"),
                GetColumnInfo(x => x.BirthPlace, caption: "Место рождения"),
                GetColumnInfo(x => x.Sex, ColumnType.Radio, caption: "Пол"),

                GetColumnInfo(x => x.HomePhone, ColumnType.MaskedText, stringMask: "0 - 00 - 00", caption: "Телефон дом"),
                GetColumnInfo(x => x.MobilePhone, ColumnType.MaskedText, stringMask: "+375(00) 000 - 00 - 00", caption: "Телофон моб."),
                GetColumnInfo(x => x.Email, ColumnType.Hyperlink, caption: "E-mail"),

                GetColumnInfo(x => x.PassportSeries, caption: "Серия паспорта"),
                GetColumnInfo(x => x.PassportNumber, ColumnType.MaskedText, stringMask: ">LL0000000", caption: "№ паспорта"),
                GetColumnInfo(x => x.IdentNumber, ColumnType.MaskedText, stringMask: "AAAAAAAAAAAAAA", caption: "Идент.номер"),
                GetColumnInfo(x => x.IssuedBy, caption: "Кем выдан"),
                GetColumnInfo(x => x.IssueDate, ColumnType.DateTime, caption: "Дата выдачи"),

                GetColumnInfo(x => x.RegistrationCity, ColumnType.ComboBox, ClientRepository.GetNames<City>(), caption: "Город прописки"),
                GetColumnInfo(x => x.RegistrationAdress, caption: "Адрес прописки"),
                GetColumnInfo(x => x.ResidenseCity, ColumnType.ComboBox, ClientRepository.GetNames<City>(), caption: "Город факт.проживания"),
                GetColumnInfo(x => x.ResidenseAdress, caption: "Адрес факт.проживания"),

                GetColumnInfo(x => x.Disability, ColumnType.ComboBox, ClientRepository.GetNames<Disability>(), caption: "Инвалидность"),
                GetColumnInfo(x => x.Nationality, ColumnType.ComboBox, ClientRepository.GetNames<Nationality>(), caption: "Гражданство"),
                GetColumnInfo(x => x.FamilyStatus, ColumnType.ComboBox, ClientRepository.GetNames<FamilyStatus>(), caption: "Семейное положение"),

                GetColumnInfo(x => x.IsPensioner, ColumnType.CheckBox, caption: "Пенсионер"),
                GetColumnInfo(x => x.IsReservist, ColumnType.CheckBox, caption: "Военнообязанный"),

                GetColumnInfo(x => x.MonthlyIncome, ColumnType.Double, caption: "Ежемесячный доход"),
                GetColumnInfo(x => x.Currency, ColumnType.ComboBox, ClientRepository.GetNames<Currency>(), caption: "Валюта"),
            };

            Grid.Columns.Clear();
            foreach (ColumnInfo columnInfo in columns)
            {
                Grid.Columns.Add(columnInfo.GetColumn());
            }
        }

        private static ColumnInfo GetColumnInfo<T>(Expression<Func<ObservableRow, T>> p, ColumnType columnType = ColumnType.Text,
            IEnumerable<object> itemSource = null, string stringMask = null, string caption = null)
        {
            return new ColumnInfo(TypeHelper.GetPropertyName(new ObservableRow(), p), columnType, caption)
            {
                ItemSource = itemSource,
                InputMask = stringMask,
            };
        }
    }
}
