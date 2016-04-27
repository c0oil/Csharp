using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Test.ViewModel;

namespace Test.Table.Observable
{
    public class ObservableValue<T> : ObservableObject
    {
        public ObservableValue()
        {
            Value = default(T);
        }

        public ObservableValue(T initValue)
        {
            Value = initValue;
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged(() => Value);
            }
        }
    }

    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanged == null)
                return;
            string name = TypeHelper.GetPropertyName(propertyExpression);
            TypeHelper.VerifyPropertyName(this, name);
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;
            TypeHelper.VerifyPropertyName(this, propertyName);
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NotifyAllChanged()
        {
            if (PropertyChanged == null)
                return;
            foreach (string property in TypeHelper.GetProperties(this))
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}