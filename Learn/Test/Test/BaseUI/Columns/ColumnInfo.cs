using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using CodeFirst;
using Test.ViewModel;

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
        Radio,
    }

    public class ColumnInfo
    {
        public ColumnInfo(string bindingPath, ColumnType columnType = ColumnType.Text, string caption = null)
        {
            Initialize(bindingPath, columnType, caption);
        }

        private void Initialize(string bindingPath, ColumnType columnType, string caption)
        {
            Caption = caption ?? bindingPath;
            ColumnType = columnType;

            BindingPath = bindingPath + ".Value";
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
                    result = new DataGridTextColumn
                    {
                        Binding = new Binding(BindingPath)
                        {
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                            ValidatesOnDataErrors = true,
                            NotifyOnValidationError = true,
                        },
                    };
                    break;
                case ColumnType.DateTime:
                    result = new DataGridDateColumn
                    {
                        Binding = new Binding(BindingPath)
                        {
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        }
                    };
                    break;
                case ColumnType.Radio:
                    result = new DataGridRadioColumn
                    {
                        Binding = new Binding(BindingPath)
                        {
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        }
                    };
                    break;
                case ColumnType.Hyperlink:
                    result = new DataGridHyperlinkColumn
                    {
                        Binding = new Binding(BindingPath)
                        {
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        }
                    };
                    break;
                case ColumnType.ComboBox:
                    result = new DataGridItemSourceColumn
                    {
                        ItemSource = new ObservableCollection<KeyValuePair<object, string>>(ItemSource.Select(x => 
                                new KeyValuePair<object, string>(x, x is Sex ? EnumHelper.ToString((Sex)x) : x.ToString()))),
                        SelectedItemBinding = new Binding(BindingPath)
                        {
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                            ValidatesOnDataErrors = true,
                            NotifyOnValidationError = true,
                        }, 
                    };
                    break;
                case ColumnType.CheckBox:
                    result = new DataGridCheckBoxColumn
                    {
                        Binding = new Binding(BindingPath)
                        {
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        }
                    };
                    break;
                case ColumnType.Double:
                    result = new DataGridTextColumn
                    {
                        Binding = new Binding(BindingPath)
                        {
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                            ValidatesOnDataErrors = true,
                            NotifyOnValidationError = true,
                            Converter = new NullableDoubleToStringConverter(),
                        }
                    };
                    break;
                case ColumnType.MaskedText:
                    result = new DataGridMaskedTextColumn
                    {
                        Binding = new Binding(BindingPath)
                        {
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                            ValidatesOnDataErrors = true,
                            NotifyOnValidationError = true,
                        }, 
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