using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CodeFirst;

namespace Test.BaseUI
{
    public partial class RadioSexControl
    {
        public static readonly DependencyProperty SelectedEnumProperty =
            DependencyProperty.Register("SelectedEnum", typeof(Enum), typeof(RadioSexControl), new FrameworkPropertyMetadata(Sex.Male));

        public Enum SelectedEnum
        {
            get { return (Enum)GetValue(SelectedEnumProperty); }
            set { SetValue(SelectedEnumProperty, value); }
        }

        public RadioSexControl()
        {
            InitializeComponent();

            var converter = new EnumToBooleanConverter();

            maleRadio.SetBinding(RadioButton.IsCheckedProperty, new Binding("Sex.Value")
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = converter,
                ConverterParameter = Sex.Male,
            });
            femaleRadio.SetBinding(RadioButton.IsCheckedProperty, new Binding("Sex.Value")
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = converter,
                ConverterParameter = Sex.Female,
            });
        }
    }
}
