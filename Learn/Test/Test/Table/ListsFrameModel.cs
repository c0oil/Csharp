using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CodeFirst;
using Test.ViewModel;

namespace Test.Table
{
    public class ListsFrameModel : ViewModelBase
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
            if (string.IsNullOrWhiteSpace(SelectedItemName))
            {
                MessageBox.Show("Plese, input correct name", "Add", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            ClientRepository.Add(SelectedItemName, SelectedList);
            RefreshItems();
        }

        private void OnUpdateRow()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Plese, select item", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(SelectedItemName))
            {
                MessageBox.Show("Plese, input correct name", "Add", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            ClientRepository.Update(SelectedItem.Key, SelectedItemName, SelectedList);
            RefreshItems();
        }

        private void OnDeleteRow()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Plese, select item", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            ClientRepository.Delete(SelectedItem.Key, SelectedList);
            RefreshItems();
        }

        #endregion

        public Dictionary<ClientList, string> Lists { get; set; }
        public ObservableCollection<ListItem> ListItems { get; set; }
        public ClientRepository ClientRepository { get; set; }

        private ClientList selectedList;
        public ClientList SelectedList
        {
            get { return selectedList; }
            set
            {
                selectedList = value;
                RefreshItems();
                OnPropertyChanged(() => SelectedList);
            }
        }

        private ListItem selectedItem;
        public ListItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                SelectedItemName = value != null ? value.Value : string.Empty;
                OnPropertyChanged(() => SelectedItem);
            }
        }

        private string selectedItemName;
        public string SelectedItemName
        {
            get { return selectedItemName; }
            set
            {
                selectedItemName = value;
                OnPropertyChanged(() => SelectedItemName);
            }
        }

        public ListsFrameModel()
        {
            ListItems = new ObservableCollection<ListItem>();
            Lists = new Dictionary<ClientList, string>
            {
                { ClientList.City, "Город" },
                { ClientList.FamilyStatus, "Семейное положение" },
                { ClientList.Currency, "Валюта" },
                { ClientList.Disability, "Инвалидность" },
                { ClientList.Nationality, "Национальность" },
            };
        }

        public void RefreshItems()
        {
            if (ClientRepository == null)
            {
                return;
            }

            var items = ClientRepository.GetList(SelectedList);
            ListItems.Clear();
            foreach (ListItem item in items.Select(x => new ListItem(x.Id, x.Name)))
            {
                ListItems.Add(item);
            }
        }

        public class ListItem
        {
            public ListItem(int key, string value)
            {
                Key = key;
                Value = value;
            }

            public int Key { get; set; }
            public string Value { get; set; }
        }
    }
}