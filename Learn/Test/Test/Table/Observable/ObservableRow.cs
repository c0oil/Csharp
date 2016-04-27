using System;
using CodeFirst;

namespace Test.Table.Observable
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

        public ValidableValue<string> ResidenseCity { get; set; }
        public ValidableValue<string> ResidenseAdress { get; set; }

        public ValidableValue<string> Disability { get; set; }
        public ValidableValue<string> Nationality { get; set; }
        public ValidableValue<string> FamilyStatus { get; set; }

        public ObservableValue<bool> IsPensioner { get; set; }
        public ObservableValue<bool> IsReservist { get; set; }

        public DoubleValidableValue MonthlyIncome { get; set; }
        public ObservableValue<string> Currency { get; set; }

        public const int NewId = -1;
        public bool IsNew
        {
            get { return ClientId == NewId; }
        }

        public bool HasError
        {
            get
            {
                return Surname.HasError || Name.HasError || MiddleName.HasError || BirthPlace.HasError ||
                       PassportSeries.HasError || PassportNumber.HasError || IdentNumber.HasError || IssuedBy.HasError ||
                       ResidenseCity.HasError || ResidenseAdress.HasError ||
                       Disability.HasError || Nationality.HasError || FamilyStatus.HasError || MonthlyIncome.HasError;
            }
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

                ResidenseCity = new ValidableValue<string>(client.ResidenseCity),
                ResidenseAdress = new ValidableValue<string>(client.ResidenseAdress),
                Disability = new ValidableValue<string>(client.Disability),
                Nationality = new ValidableValue<string>(client.Nationality),
                FamilyStatus = new ValidableValue<string>(client.FamilyStatus),
                IsPensioner = new ObservableValue<bool>(client.IsPensioner),
                IsReservist = new ObservableValue<bool>(client.IsReservist),
                MonthlyIncome = new DoubleValidableValue(client.MonthlyIncome.ToString()),
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

                ResidenseCity = (client.ResidenseCity.Value),
                ResidenseAdress = (client.ResidenseAdress.Value),
                Disability = (client.Disability.Value),
                Nationality = (client.Nationality.Value),
                FamilyStatus = (client.FamilyStatus.Value),
                IsPensioner = (client.IsPensioner.Value),
                IsReservist = (client.IsReservist.Value),
                MonthlyIncome = string.IsNullOrWhiteSpace(client.MonthlyIncome.Value)
                                    ? (double?) null
                                    : Double.Parse(client.MonthlyIncome.Value),
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

                ResidenseCity = new ValidableValue<string>(),
                ResidenseAdress = new ValidableValue<string>(),
                Disability = new ValidableValue<string>(),
                Nationality = new ValidableValue<string>(),
                FamilyStatus = new ValidableValue<string>(),
                IsPensioner = new ObservableValue<bool>(),
                IsReservist = new ObservableValue<bool>(),
                MonthlyIncome = new DoubleValidableValue(),
                Currency = new ObservableValue<string>(),
            };
        }
    }
}