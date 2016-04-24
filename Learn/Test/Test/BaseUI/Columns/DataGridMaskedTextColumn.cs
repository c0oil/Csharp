using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace Test.BaseUI.Columns
{
    public class DataGridMaskedTextColumn : DataGridTemplateColumn
    {
        private readonly FrameworkElementFactory factory;
        public DataGridMaskedTextColumn()
        {
            factory = new FrameworkElementFactory(typeof(TextBox));
            CellTemplate = new DataTemplate
            {
                VisualTree = factory,
            };
        }

        private Binding binding;
        public Binding Binding
        {
            get { return binding; }
            set
            {
                if (binding == value)
                {
                    return;
                }
                binding = value;
                factory.SetBinding(TextBox.TextProperty, value);
                NotifyPropertyChanged("Binding");
            }
        }

        private string inputMask;
        public string InputMask
        {
            get { return inputMask; }
            set
            {
                inputMask = value;
                factory.AddHandler(TextBox.LoadedEvent, new RoutedEventHandler(Loaded));
                NotifyPropertyChanged("InputMask");
            }
        }

        private void Loaded(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }
            
            TextBoxInputMaskBehavior behavior = new TextBoxInputMaskBehavior();
            behavior.SetValue(TextBoxInputMaskBehavior.InputMaskProperty, InputMask);
            Interaction.GetBehaviors(textBox).Add(behavior);

            textBox.Loaded -= Loaded;
        }
    }
}