using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Test.BaseUI.Columns
{
    public class DataGridDateColumn : DataGridTemplateColumn
    {
        private readonly FrameworkElementFactory factory;
        public DataGridDateColumn()
        {
            factory = new FrameworkElementFactory(typeof(DatePicker));
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
                factory.SetBinding(DatePicker.SelectedDateProperty, value);
                NotifyPropertyChanged("Binding");
            }
        }
    }
}