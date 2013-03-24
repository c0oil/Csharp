using System.Windows.Input;

namespace cosii5
{
    public partial class ImageRedactor
    {  
        public ImageRedactor()
        {
            InitializeComponent();
            DataContext = ViewModel;

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, ViewModel.OpenExecuted, ViewModel.CanOpen));
        }

        ImageRedactorModel ViewModel = new ImageRedactorModel();
    }
}
