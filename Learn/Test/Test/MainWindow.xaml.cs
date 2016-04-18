using Test.BaseUI;

namespace Test
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class MainWindowBase : AsWindow<MainWindowViewModel> {}
}
