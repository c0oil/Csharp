using System;
using System.Windows;
using CodeFirst;
using Test.BaseUI;

namespace Test.Table
{
    public partial class ListsFrame
    {
        public ListsFrame()
        {
            InitializeComponent();
        }
    }
    public class ListsFrameBase : AsUserControl<ListsFrameModel> { }
}