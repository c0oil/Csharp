using System;
using System.Windows;
using Test.BaseUI;
using Test.ViewModel;

namespace Test.Table
{
    public partial class TableFrame
    {
        public TableFrame()
        {
            InitializeComponent();
            ViewModel.Grid = grid;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UiHelper.ExecuteAndCatchException(ViewModel.Refresh);
        }
    }
}
