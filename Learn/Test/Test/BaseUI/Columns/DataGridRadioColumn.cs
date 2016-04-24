using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Test.BaseUI.Columns
{
    public class DataGridRadioColumn : DataGridTemplateColumn
    {
        private readonly FrameworkElementFactory factory;
        public DataGridRadioColumn()
        {
            factory = new FrameworkElementFactory(typeof(RadioSexControl));
            CellTemplate = new DataTemplate
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
                factory.SetBinding(RadioSexControl.SelectedEnumProperty, value);
                NotifyPropertyChanged("Binding");
            }
        }
    }
}