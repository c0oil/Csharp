using System;
using System.Windows;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Test.ViewModel
{
    public abstract class ViewModelBase: DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> p)
        {
            OnPropertyChanged(TypeHelper.GetPropertyName(p));
        }

        public static ICommand GetDelegateCommand<T>(ref ICommand existingCommand, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            return existingCommand ?? (existingCommand = new DelegateCommand<T>(action) { IsEnabled = true });
        }
    }
}
