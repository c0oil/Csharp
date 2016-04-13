using System.Runtime.CompilerServices;
using System.Windows;
using Test.ViewModel;

namespace Test.BaseUI
{
    public class AsWindow<T> : Window where T : ViewModelBase, new()
    {
        public AsWindow()
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
