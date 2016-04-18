using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Test.ViewModel;

namespace Test.BaseUI
{
    public class AsUserControl<T> : UserControl where T : ViewModelBase, new()
    {
        public AsUserControl()
        {
            ViewModel = new T();
        }

        public T ViewModel
        {
            get { return (T) DataContext; }
            set { DataContext = value; }
        }
    }
}
