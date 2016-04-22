using System;
using System.Windows;
using Test.BaseUI;

namespace Test.Table
{
    public partial class TableView
    {
        public TableView()
        {
            InitializeComponent();
            ViewModel.Grid = grid;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TableViewModel.ExecuteAndCatchException(ViewModel.Refresh);
        }
    }

    public class TableViewBase : AsWindow<TableViewModel> { }
}
