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
        public ObservableItemSourseValue<Sex> Sex { get; set; }

        public ObservableValue<string> HomePhone { get; set; } //mask
        public ObservableValue<string> MobilePhone { get; set; } //mask
        public ObservableValue<string> Email { get; set; }

        public ObservableValue<string> PassportSeries { get; set; }
        public ObservableValue<string> PassportNumber { get; set; } //mask
        public ObservableValue<string> IdentNumber { get; set; } //mask
        public ObservableValue<string> IssuedBy { get; set; }
        public ObservableValue<DateTime> IssueDate { get; set; }

        public ObservableItemSourseValue<string> RegistrationCity { get; set; }
        public ObservableValue<string> RegistrationAdress { get; set; }
        public ObservableItemSourseValue<string> ResidenseCity { get; set; }
        public ObservableValue<string> ResidenseAdress { get; set; }

        public ObservableItemSourseValue<string> Disability { get; set; }
        public ObservableItemSourseValue<string> Nationality { get; set; }
        public ObservableItemSourseValue<string> FamilyStatus { get; set; }

        public ObservableValue<bool> IsPensioner { get; set; }
        public ObservableValue<bool> IsReservist { get; set; }

        public ObservableValue<double> MonthlyIncome { get; set; }
        public ObservableItemSourseValue<string> Currency { get; set; }

        public static ObservableRow ConvertToRow(ClientObj client, 
            IEnumerable<Sex> sexes, IEnumerable<string> cities, IEnumerable<string> disabilities,
            IEnumerable<string> nationalities, IEnumerable<string> familyStatuses, IEnumerable<string> currencies)
        {
            return new ObservableRow
            {
                ClientId = client.ClientId,

                Surname = new ObservableValue<string>(client.Surname),
                Name = new ObservableValue<string>(client.Name),
                MiddleName = new ObservableValue<string>(client.MiddleName),
                BirthDate = new ObservableValue<DateTime>(client.BirthDate),
                BirthPlace = new ObservableValue<string>(client.BirthPlace),
                Sex = new ObservableItemSourseValue<Sex>(client.Sex, sexes),

                HomePhone = new ObservableValue<string>(client.HomePhone),
                MobilePhone = new ObservableValue<string>(client.MobilePhone),
                Email = new ObservableValue<string>(client.Email),

                PassportSeries = new ObservableValue<string>(client.PassportSeries),
                PassportNumber = new ObservableValue<string>(client.PassportNumber),
                IdentNumber = new ObservableValue<string>(client.IdentNumber),
                IssuedBy = new ObservableValue<string>(client.IssuedBy),
                IssueDate = new ObservableValue<DateTime>(client.IssueDate),

                RegistrationCity = new ObservableItemSourseValue<string>(client.RegistrationCity, cities),
                RegistrationAdress = new ObservableValue<string>(client.RegistrationAdress),
                ResidenseCity = new ObservableItemSourseValue<string>(client.ResidenseCity, cities),
                ResidenseAdress = new ObservableValue<string>(client.ResidenseAdress),
                Disability = new ObservableItemSourseValue<string>(client.Disability, disabilities),
                Nationality = new ObservableItemSourseValue<string>(client.Nationality, nationalities),
                FamilyStatus = new ObservableItemSourseValue<string>(client.FamilyStatus, familyStatuses),
                IsPensioner = new ObservableValue<bool>(client.IsPensioner),
                IsReservist = new ObservableValue<bool>(client.IsReservist),
                MonthlyIncome = new ObservableValue<double>(client.MonthlyIncome ?? 0.0),
                Currency = new ObservableItemSourseValue<string>(client.Currency, currencies),
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

        public static ObservableRow GetEmptyRow(IEnumerable<Sex> sexes, IEnumerable<string> cities, IEnumerable<string> disabilities,
                                                IEnumerable<string> nationalities, IEnumerable<string> familyStatuses, IEnumerable<string> currencies)
        {
            return new ObservableRow
            {
                ClientId = -1,

                Surname = new ObservableValue<string>(),
                Name = new ObservableValue<string>(),
                MiddleName = new ObservableValue<string>(),
                BirthDate = new ObservableValue<DateTime>(),
                BirthPlace = new ObservableValue<string>(),
                Sex = new ObservableItemSourseValue<Sex>(sexes),

                HomePhone = new ObservableValue<string>(),
                MobilePhone = new ObservableValue<string>(),
                Email = new ObservableValue<string>(),

                PassportSeries = new ObservableValue<string>(),
                PassportNumber = new ObservableValue<string>(),
                IdentNumber = new ObservableValue<string>(),
                IssuedBy = new ObservableValue<string>(),
                IssueDate = new ObservableValue<DateTime>(),

                RegistrationCity = new ObservableItemSourseValue<string>(cities),
                RegistrationAdress = new ObservableValue<string>(),
                ResidenseCity = new ObservableItemSourseValue<string>(cities),
                ResidenseAdress = new ObservableValue<string>(),
                Disability = new ObservableItemSourseValue<string>(disabilities),
                Nationality = new ObservableItemSourseValue<string>(nationalities),
                FamilyStatus = new ObservableItemSourseValue<string>(familyStatuses),
                IsPensioner = new ObservableValue<bool>(),
                IsReservist = new ObservableValue<bool>(),
                MonthlyIncome = new ObservableValue<double>(0.0),
                Currency = new ObservableItemSourseValue<string>(currencies),
            };
        }
    }

    public class ObservableItemSourseValue<T> : ObservableValue<T>
    {
        public ObservableItemSourseValue(IEnumerable<T> itemSource)
        {
            ItemSource = itemSource.Select(x => new KeyValuePair<T, string>(x, x.ToString()));
        }

        public ObservableItemSourseValue(T initValue, IEnumerable<T> itemSource)
            : base(initValue)
        {
            ItemSource = itemSource.Select(x => new KeyValuePair<T, string>(x, x.ToString()));
        }

        public ObservableItemSourseValue(T initValue, IEnumerable<KeyValuePair<T, string>> itemSource)
            : base(initValue)
        {
            ItemSource = itemSource;
        }

        private IEnumerable<KeyValuePair<T, string>> _itemSource;
        public IEnumerable<KeyValuePair<T, string>> ItemSource
        {
            get { return _itemSource; }
            set
            {
                _itemSource = value;
                OnPropertyChanged(() => ItemSource);
            }
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