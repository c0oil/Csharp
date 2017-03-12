using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Test.BaseUI.Columns
{
    public class DataGridItemSourceColumn : DataGridTemplateColumn
    {
        private readonly FrameworkElementFactory factory;
        public DataGridItemSourceColumn()
        {
            factory = new FrameworkElementFactory(typeof(ComboBox));
            CellTemplate = new DataTemplate
            {
                VisualTree = factory,
            };
            factory.AddHandler(ComboBox.LoadedEvent, new RoutedEventHandler(Loaded));
        }

        private BindingBase selectedItemBinding;
        public BindingBase SelectedItemBinding
        {
            get { return selectedItemBinding; }
            set
            {
                if (selectedItemBinding == value)
                {
                    return;
                }
                selectedItemBinding = value;
                factory.SetValue(ComboBox.DisplayMemberPathProperty, "Value");
                factory.SetValue(ComboBox.SelectedValuePathProperty, "Key");
                factory.SetBinding(ComboBox.SelectedValueProperty, value);
                NotifyPropertyChanged("SelectedItemBinding");
            }
        }

        private ObservableCollection<KeyValuePair<object, string>> itemSource;
        public ObservableCollection<KeyValuePair<object, string>> ItemSource
        {
            get { return itemSource; }
            set
            {
                if (itemSource == value)
                {
                    return;
                }
                itemSource = value;
                factory.SetBinding(ComboBox.ItemsSourceProperty, new Binding("Column.ItemSource")
                {
                    RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGridCell), 1)
                });
                NotifyPropertyChanged("ItemSource");
            }
        }

        private void Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null)
            {
                return;
            }

            if (comboBox.SelectedValue == null)
            {
                comboBox.SelectedValue = ItemSource.First().Key;
            }
            comboBox.Loaded -= Loaded;
        }
    }
}