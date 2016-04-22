using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace Test.BaseUI.Columns
{
    public enum ColumnType
    {
        Text,
        MaskedText,
        Double,
        DateTime,
        Hyperlink,
        ComboBox,
        CheckBox,
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
        }

        public string Caption { get; set; }
        public ColumnType ColumnType { get; set; }

        public string BindingPath { get; set; }
        public string InputMask { get; set; }
        public IEnumerable<object> ItemSource { get; set; }

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
                        ItemSource = new ObservableCollection<KeyValuePair<object, string>>(ItemSource.Select(x => new KeyValuePair<object, string>(x, x.ToString()))),
                        SelectedItemBinding = new Binding(BindingPath) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged }, 
                    };
                    break;
                case ColumnType.CheckBox:
                    result = new DataGridCheckBoxColumn { Binding = new Binding(BindingPath) };
                    break;
                case ColumnType.Double:
                    result = new DataGridTextColumn { Binding = new Binding(BindingPath) };
                    break;
                case ColumnType.MaskedText:
                    result = new DataGridMaskedTextColumn
                    {
                        Binding = new Binding(BindingPath) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged }, 
                        InputMask = InputMask
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            result.Header = Caption;
            return result;
        }
    }
}