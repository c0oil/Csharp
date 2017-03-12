using System.Windows;
using Test.ViewModel;

namespace Test.BaseUI
{
    public class AsWindow<T> : Window where T : ViewModelBase, new()
    {
        public AsWindow()
        {
            ViewModel = new T();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            bool? dialogResult = null;
            Closing += (sender, e) =>
            {
                if (DialogResult != dialogResult)
                {
                    DialogResult = dialogResult;
                }
            };
            ViewModel.OnCloseView = result =>
            {
                dialogResult = result;
                Close();
            };
        }

        public T ViewModel
        {
            get { return (T) DataContext; }
            set { DataContext = value; }
        }
    }
}
