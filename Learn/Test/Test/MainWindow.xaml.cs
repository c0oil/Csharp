using Test.BaseUI;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            InitGrid();
        }

        private void InitGrid()
        {
            grid.CanUserAddRows = true;
            grid.CanUserDeleteRows = true;
            //grid.Columns.Add(new DataGridTextColumn { Header = "City", Binding = new Binding("City") });
            
        }
    }

    public class MainWindowBase : AsWindow<MainWindowViewModel> {}
}
