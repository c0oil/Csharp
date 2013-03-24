using System;
using System.Windows;
using System.ComponentModel;

namespace cosii5.MVVMUtility
{
    public abstract class ViewModelBase
        : DependencyObject, INotifyPropertyChanged
    {/*
        public bool IsDesignTime
        {
            get { return DesignerProperties.IsInDesignTool; }
        }*/
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
