using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using CodeFirst;
using Test.BaseUI;
using Test.ViewModel;

namespace Test.Table
{
    public class ObservableRow
    {
        public int ClientId { get; set; }

        public ValidableValue<string> Surname { get; set; }
        public ValidableValue<string> Name { get; set; }
        public ValidableValue<string> MiddleName { get; set; }
        public ObservableValue<DateTime> BirthDate { get; set; }
        public ValidableValue<string> BirthPlace { get; set; }
        public ObservableValue<Sex> Sex { get; set; }

        public ObservableValue<string> HomePhone { get; set; }
        public ObservableValue<string> MobilePhone { get; set; }
        public ObservableValue<string> Email { get; set; }

        public ValidableValue<string> PassportSeries { get; set; }
        public MaskedValidableValue PassportNumber { get; set; }
        public MaskedValidableValue IdentNumber { get; set; }
        public ValidableValue<string> IssuedBy { get; set; }
        public ObservableValue<DateTime> IssueDate { get; set; }

        public ValidableValue<string> RegistrationCity { get; set; }
        public ValidableValue<string> RegistrationAdress { get; set; }
        public ValidableValue<string> ResidenseCity { get; set; }
        public ValidableValue<string> ResidenseAdress { get; set; }

        public ValidableValue<string> Disability { get; set; }
        public ValidableValue<string> Nationality { get; set; }
        public ValidableValue<string> FamilyStatus { get; set; }

        public ObservableValue<bool> IsPensioner { get; set; }
        public ObservableValue<bool> IsReservist { get; set; }

        public ObservableValue<double?> MonthlyIncome { get; set; }
        public ObservableValue<string> Currency { get; set; }

        public const int NewId = -1;
        public bool IsNew
        {
            get { return ClientId == NewId; }
        }

        public static ObservableRow ConvertToRow(ClientObj client)
        {
            return new ObservableRow
            {
                ClientId = client.ClientId,

                Surname = new ValidableValue<string>(client.Surname),
                Name = new ValidableValue<string>(client.Name),
                MiddleName = new ValidableValue<string>(client.MiddleName),
                BirthDate = new ObservableValue<DateTime>(client.BirthDate),
                BirthPlace = new ValidableValue<string>(client.BirthPlace),
                Sex = new ObservableValue<Sex>(client.Sex),

                HomePhone = new ObservableValue<string>(client.HomePhone),
                MobilePhone = new ObservableValue<string>(client.MobilePhone),
                Email = new ObservableValue<string>(client.Email),

                PassportSeries = new ValidableValue<string>(client.PassportSeries),
                PassportNumber = new MaskedValidableValue(client.PassportNumber),
                IdentNumber = new MaskedValidableValue(client.IdentNumber),
                IssuedBy = new ValidableValue<string>(client.IssuedBy),
                IssueDate = new ObservableValue<DateTime>(client.IssueDate),

                RegistrationCity = new ValidableValue<string>(client.RegistrationCity),
                RegistrationAdress = new ValidableValue<string>(client.RegistrationAdress),
                ResidenseCity = new ValidableValue<string>(client.ResidenseCity),
                ResidenseAdress = new ValidableValue<string>(client.ResidenseAdress),
                Disability = new ValidableValue<string>(client.Disability),
                Nationality = new ValidableValue<string>(client.Nationality),
                FamilyStatus = new ValidableValue<string>(client.FamilyStatus),
                IsPensioner = new ObservableValue<bool>(client.IsPensioner),
                IsReservist = new ObservableValue<bool>(client.IsReservist),
                MonthlyIncome = new ObservableValue<double?>(client.MonthlyIncome),
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

                HomePhone = MaskedValidableValue.SkipWrongMaskedValue(client.HomePhone.Value),
                MobilePhone = MaskedValidableValue.SkipWrongMaskedValue(client.MobilePhone.Value),
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
                ClientId = NewId,

                Surname = new ValidableValue<string>(),
                Name = new ValidableValue<string>(),
                MiddleName = new ValidableValue<string>(),
                BirthDate = new ObservableValue<DateTime>(new DateTime(1990, 1, 1)),
                BirthPlace = new ValidableValue<string>(),
                Sex = new ObservableValue<Sex>(CodeFirst.Sex.Male),

                HomePhone = new ObservableValue<string>(),
                MobilePhone = new ObservableValue<string>(),
                Email = new ObservableValue<string>(),

                PassportSeries = new ValidableValue<string>(),
                PassportNumber = new MaskedValidableValue(),
                IdentNumber = new MaskedValidableValue(),
                IssuedBy = new ValidableValue<string>(),
                IssueDate = new ObservableValue<DateTime>(new DateTime(1990, 1, 1)),

                RegistrationCity = new ValidableValue<string>(),
                RegistrationAdress = new ValidableValue<string>(),
                ResidenseCity = new ValidableValue<string>(),
                ResidenseAdress = new ValidableValue<string>(),
                Disability = new ValidableValue<string>(),
                Nationality = new ValidableValue<string>(),
                FamilyStatus = new ValidableValue<string>(),
                IsPensioner = new ObservableValue<bool>(),
                IsReservist = new ObservableValue<bool>(),
                MonthlyIncome = new ObservableValue<double?>(),
                Currency = new ObservableValue<string>(),
            };
        }
    }

    public class MaskedValidableValue : ObservableValue<string>, IDataErrorInfo
    {
        public MaskedValidableValue() { }
        public MaskedValidableValue(string initValue) : base(initValue) { }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Value" && (string.IsNullOrEmpty(Value) || Value.Contains(TextBoxInputMaskBehavior.DefaultPromptChar)))
                {
                    result = "Please enter Value";
                }
                return result;
            }
        }

        public static string SkipWrongMaskedValue(string val)
        {
            return val.Contains(TextBoxInputMaskBehavior.DefaultPromptChar) ? string.Empty : val;
        }

        public string Error
        {
            get { return "Please enter Value"; }
        }
    }

    public class ValidableValue<T> : ObservableValue<T>, IDataErrorInfo
    {
        public ValidableValue() { }
        public ValidableValue(T initValue): base(initValue) { }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Value" && (string.IsNullOrEmpty(Value as string)))
                {
                    result = "Please enter Value";
                }
                return result;
            }
        }

        public string Error
        {
            get { return "Please enter Value"; }
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