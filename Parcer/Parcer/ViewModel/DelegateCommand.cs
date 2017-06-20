using System;
using System.Windows.Input;

namespace Parcer.ViewModel
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> handler;
        private bool isEnabled;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> handler)
        {
            this.handler = handler;
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (value != isEnabled)
                {
                    isEnabled = value;
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool CanExecute(T parameter)
        {
            return IsEnabled;
        }

        public void Execute(T parameter)
        {
            handler(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }
    }
}
