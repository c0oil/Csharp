using Test.BaseUI;

namespace Test.Table
{
    public partial class TableView
    {
        public TableView()
        {
            InitializeComponent();

            ViewModel.ListsFrame = listsFrame;
            ViewModel.TableFrame = tableFrame;
        }
    }
}
