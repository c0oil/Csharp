using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using CodeFirst;
using Test.ViewModel;

namespace Test.Table
{
    public class ObservableRow
    {
        public int ClientId { get; set; }

        public ObservableValue<string> Surname { get; set; }
        public ObservableValue<string> Name { get; set; }
        public ObservableValue<string> MiddleName { get; set; }
        public ObservableValue<DateTime> BirthDate { get; set; }
        public ObservableValue<string> BirthPlace { get; set; }
        public ObservableValue<Sex> Sex { get; set; }

        public ObservableValue<string> HomePhone { get; set; }
        public ObservableValue<string> MobilePhone { get; set; }
        public ObservableValue<string> Email { get; set; }

        public ObservableValue<string> PassportSeries { get; set; }
        public ObservableValue<string> PassportNumber { get; set; }
        public ObservableValue<string> IdentNumber { get; set; }
        public ObservableValue<string> IssuedBy { get; set; }
        public ObservableValue<DateTime> IssueDate { get; set; }

        public ObservableValue<string> RegistrationCity { get; set; }
        public ObservableValue<string> RegistrationAdress { get; set; }
        public ObservableValue<string> ResidenseCity { get; set; }
        public ObservableValue<string> ResidenseAdress { get; set; }

        public ObservableValue<string> Disability { get; set; }
        public ObservableValue<string> Nationality { get; set; }
        public ObservableValue<string> FamilyStatus { get; set; }

        public ObservableValue<bool> IsPensioner { get; set; }
        public ObservableValue<bool> IsReservist { get; set; }

        public ObservableValue<double> MonthlyIncome { get; set; }
        public ObservableValue<string> Currency { get; set; }

        public static ObservableRow ConvertToRow(ClientObj client)
        {
            return new ObservableRow
            {
                ClientId = client.ClientId,

                Surname = new ObservableValue<string>(client.Surname),
                Name = new ObservableValue<string>(client.Name),
                MiddleName = new ObservableValue<string>(client.MiddleName),
                BirthDate = new ObservableValue<DateTime>(client.BirthDate),
                BirthPlace = new ObservableValue<string>(client.BirthPlace),
                Sex = new ObservableValue<Sex>(client.Sex),

                HomePhone = new ObservableValue<string>(client.HomePhone),
                MobilePhone = new ObservableValue<string>(client.MobilePhone),
                Email = new ObservableValue<string>(client.Email),

                PassportSeries = new ObservableValue<string>(client.PassportSeries),
                PassportNumber = new ObservableValue<string>(client.PassportNumber),
                IdentNumber = new ObservableValue<string>(client.IdentNumber),
                IssuedBy = new ObservableValue<string>(client.IssuedBy),
                IssueDate = new ObservableValue<DateTime>(client.IssueDate),

                RegistrationCity = new ObservableValue<string>(client.RegistrationCity),
                RegistrationAdress = new ObservableValue<string>(client.RegistrationAdress),
                ResidenseCity = new ObservableValue<string>(client.ResidenseCity),
                ResidenseAdress = new ObservableValue<string>(client.ResidenseAdress),
                Disability = new ObservableValue<string>(client.Disability),
                Nationality = new ObservableValue<string>(client.Nationality),
                FamilyStatus = new ObservableValue<string>(client.FamilyStatus),
                IsPensioner = new ObservableValue<bool>(client.IsPensioner),
                IsReservist = new ObservableValue<bool>(client.IsReservist),
                MonthlyIncome = new ObservableValue<double>(client.MonthlyIncome ?? 0.0),
                Currency = new ObservableValue<string>(client.Currency),
            };
        }

        public static ClientObj ConvertToObj(ObservableRow client)
        {
            return new ClientObj
            {
                ClientId = client.ClientId,

                Surname = (client.Surname.Value),
                Name = (client.Name.Value),
                MiddleName = (client.MiddleName.Value),
                BirthDate = (client.BirthDate.Value),
                BirthPlace = (client.BirthPlace.Value),
                Sex = (client.Sex.Value),

                HomePhone = (client.HomePhone.Value),
                MobilePhone = (client.MobilePhone.Value),
                Email = (client.Email.Value),

                PassportSeries = (client.PassportSeries.Value),
                PassportNumber = (client.PassportNumber.Value),
                IdentNumber = (client.IdentNumber.Value),
                IssuedBy = (client.IssuedBy.Value),
                IssueDate = (client.IssueDate.Value),

                RegistrationCity = (client.RegistrationCity.Value),
                RegistrationAdress = (client.RegistrationAdress.Value),
                ResidenseCity = (client.ResidenseCity.Value),
                ResidenseAdress = (client.ResidenseAdress.Value),
                Disability = (client.Disability.Value),
                Nationality = (client.Nationality.Value),
                FamilyStatus = (client.FamilyStatus.Value),
                IsPensioner = (client.IsPensioner.Value),
                IsReservist = (client.IsReservist.Value),
                MonthlyIncome = (client.MonthlyIncome.Value),
                Currency = (client.Currency.Value),
            };
        }

        public static ObservableRow GetEmptyRow()
        {
            return new ObservableRow
            {
                ClientId = -1,

                Surname = new ObservableValue<string>(),
                Name = new ObservableValue<string>(),
                MiddleName = new ObservableValue<string>(),
                BirthDate = new ObservableValue<DateTime>(),
                BirthPlace = new ObservableValue<string>(),
                Sex = new ObservableValue<Sex>(),

                HomePhone = new ObservableValue<string>(),
                MobilePhone = new ObservableValue<string>(),
                Email = new ObservableValue<string>(),

                PassportSeries = new ObservableValue<string>(),
                PassportNumber = new ObservableValue<string>(),
                IdentNumber = new ObservableValue<string>(),
                IssuedBy = new ObservableValue<string>(),
                IssueDate = new ObservableValue<DateTime>(),

                RegistrationCity = new ObservableValue<string>(),
                RegistrationAdress = new ObservableValue<string>(),
                ResidenseCity = new ObservableValue<string>(),
                ResidenseAdress = new ObservableValue<string>(),
                Disability = new ObservableValue<string>(),
                Nationality = new ObservableValue<string>(),
                FamilyStatus = new ObservableValue<string>(),
                IsPensioner = new ObservableValue<bool>(),
                IsReservist = new ObservableValue<bool>(),
                MonthlyIncome = new ObservableValue<double>(),
                Currency = new ObservableValue<string>(),
            };
        }
    }

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