using System;
using System.Windows;
using System.Windows.Controls;
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

    public class TableViewBase : AsWindow<TableViewModel> { }
}
