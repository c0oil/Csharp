using System;
using CodeFirst;

namespace Test.Table
{
    public class ClientObj
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public Sex Sex { get; set; }

        public string HomePhone { get; set; } //mask
        public string MobilePhone { get; set; } //mask
        public string Email { get; set; }

        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; } //mask
        public string IdentNumber { get; set; } //mask
        public string IssuedBy { get; set; }
        public DateTime IssueDate { get; set; }

        public string RegistrationCity { get; set; }
        public string RegistrationAdress { get; set; }
        public string ResidenseCity { get; set; }
        public string ResidenseAdress { get; set; }

        public string Disability { get; set; }
        public string Nationality { get; set; }
        public string FamilyStatus { get; set; }

        public bool IsPensioner { get; set; }
        public bool IsReservist { get; set; }

        public double? MonthlyIncome { get; set; }
        public string Currency { get; set; }

        public static ClientObj ConvertToRow(Client client)
        {
            return new ClientObj
            {
                Surname = (client.Surname),
                Name = (client.Name),
                MiddleName = (client.MiddleName),
                BirthDate = (client.BirthDate),
                BirthPlace = (client.BirthPlace),
                Sex = (client.Sex),

                HomePhone = (client.HomePhone),
                MobilePhone = (client.MobilePhone),
                Email = (client.Email),

                PassportSeries = (client.Passport.PassportSeries),
                PassportNumber = (client.Passport.PassportNumber),
                IdentNumber = (client.Passport.IdentNumber),
                IssuedBy = (client.Passport.IssuedBy),
                IssueDate = (client.Passport.IssueDate),

                RegistrationCity = (client.Registration.City.CityId),
                RegistrationAdress = (client.Registration.Adress),
                ResidenseCity = (client.Residense.City.CityId),
                ResidenseAdress = (client.Residense.Adress),
                Disability = (client.Disability.DisabilityId),
                Nationality = (client.Nationality.NationalityId),
                FamilyStatus = (client.FamilyStatus.FamilyStatusId),
                IsPensioner = (client.IsPensioner),
                IsReservist = (client.IsReservist),
                MonthlyIncome = (client.MonthlyIncome),
                Currency = (client.Currency.CurrencyId),
            };
        }
    }
}