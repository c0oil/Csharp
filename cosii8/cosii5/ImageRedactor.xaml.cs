using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace cosii5
{
    public partial class ImageRedactor
    {  
        public ImageRedactor()
        {
            InitializeComponent();
            DataContext = ViewModel;

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, ViewModel.OpenExecuted, ViewModel.CanOpen));

            SetSamplesToStack(1);
            SetSamplesToStack(2);
            SetSamplesToStack(3);
        }

        ImageRedactorModel ViewModel = new ImageRedactorModel();

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectSampleCommand.Execute(((ComboBox)sender).SelectedIndex);
        }

        private void SetSamplesToStack(int numSamples)
        {
            StackPanel stack;
            switch (numSamples)
            {
                case 1: stack = samples1; break;
                case 2: stack = samples2; break;
                case 3: stack = samples3; break;
                default: stack = new StackPanel(); break;
            }

            foreach (var sample in ViewModel.BitmapSamples.Where(x => (x.Key-1)/3 == numSamples - 1).Select(x => x.Value))
            {
                stack.Children.Add(new Image { Source = sample});
            }
        }
    }
}
