using System;
using System.Windows.Input;

namespace cosii5.MVVMUtility
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> handler;
        private bool isEnabled;

        public RelayCommand(Action<object> handler)
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
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            handler(parameter);
        }
    }
}
