using System.Windows.Controls;
using Parcer.ViewModel;

namespace Parcer.BaseControls
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
