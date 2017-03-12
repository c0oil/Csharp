using Test.BaseUI;

namespace Test.DbConnection
{
    public partial class ConnectionView
    {
        public ConnectionView()
        {
            InitializeComponent();
            ViewModel.ConnectionControl = connectionControl;
        }
    }

    public class ConnectionViewBase : AsWindow<ConnectionViewModel> { }
}
