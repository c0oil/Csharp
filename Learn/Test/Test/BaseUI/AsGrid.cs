using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Test.BaseUI
{
    public class AsGrid : DataGrid
    {
    }

    public class ColumnInfo
    {
        public ColumnInfo(string caption, ColumnType columnType = ColumnType.Text)
        {
            Initialize(caption, columnType);
        }

        private void Initialize(string caption, ColumnType columnType = ColumnType.Text)
        {
            Caption = caption;
            ColumnType = columnType;

            BindingPath = caption + ".Value";
            ItemSourceBindingPath = caption + ".ItemSource";
        }

        public string Caption { get; set; }
        public ColumnType ColumnType { get; set; }

        public string BindingPath { get; set; }
        public string ItemSourceBindingPath { get; set; }

        public DataGridColumn GetColumn()
        {
            DataGridColumn result = null;
            switch (ColumnType)
            {
                case ColumnType.Text:
                    result = new DataGridTextColumn { Binding = new Binding(BindingPath) };
                    break;
                case ColumnType.DateTime:
                    result = new DataGridDateColumn
                    {
                        Binding = new Binding(BindingPath) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged}
                    };
                    break;
                case ColumnType.Hyperlink:
                    result = new DataGridHyperlinkColumn { Binding = new Binding(BindingPath) };
                    break;
                case ColumnType.ComboBox:
                    result = new DataGridItemSourceColumn
                    {
                        SelectedItemBinding = new Binding(BindingPath) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged }, 
                        ItemsSourceBinding = new Binding(ItemSourceBindingPath)
                    };
                    break;
                case ColumnType.CheckBox:
                    result = new DataGridCheckBoxColumn { Binding = new Binding(BindingPath) };
                    break;
                case ColumnType.Double:
                    result = new DataGridTextColumn { Binding = new Binding(BindingPath) };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            result.Header = Caption;
            return result;
        }
    }

    public enum ColumnType
    {
        Text,
        Double,
        DateTime,
        Hyperlink,
        ComboBox,
        CheckBox,
    }

    public class DataGridDateColumn : DataGridTemplateColumn
    {
        private readonly FrameworkElementFactory factory;
        public DataGridDateColumn()
        {
            factory = new FrameworkElementFactory(typeof(DatePicker));
            CellTemplate = new DataTemplate()
            {
                VisualTree = factory,
            };
        }

        private BindingBase binding;
        public BindingBase Binding
        {
            get { return binding; }
            set
            {
                if (binding == value)
                {
                    return;
                }
                binding = value;
                factory.SetBinding(DatePicker.SelectedDateProperty, value);
                NotifyPropertyChanged("Binding");
            }
        }
    }

    public class DataGridItemSourceColumn : DataGridTemplateColumn
    {
        private readonly FrameworkElementFactory factory;
        public DataGridItemSourceColumn()
        {
            factory = new FrameworkElementFactory(typeof(ComboBox));
            CellTemplate = new DataTemplate()
            {
                VisualTree = factory,
            };
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
                factory.SetValue(ComboBox.UseLayoutRoundingProperty, false);
                factory.SetValue(ComboBox.DisplayMemberPathProperty, "Value");
                factory.SetValue(ComboBox.SelectedValuePathProperty, "Key");
                factory.SetBinding(ComboBox.SelectedValueProperty, value);
                NotifyPropertyChanged("SelectedItemBinding");
            }
        }

        private BindingBase itemsSourcebinding;
        public BindingBase ItemsSourceBinding
        {
            get { return itemsSourcebinding; }
            set
            {
                if (itemsSourcebinding == value)
                {
                    return;
                }
                itemsSourcebinding = value;
                factory.SetBinding(ComboBox.ItemsSourceProperty, value);
                NotifyPropertyChanged("ItemsSourceBinding");
            }
        }
    }
}
