using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeFirst
{
    public class ClientObj
    {
        public int ClientId { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public Sex Sex { get; set; }

        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }

        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string IdentNumber { get; set; }
        public string IssuedBy { get; set; }
        public DateTime IssueDate { get; set; }

        public string ResidenseCity { get; set; }
        public string ResidenseAdress { get; set; }

        public string Disability { get; set; }
        public string Nationality { get; set; }
        public string FamilyStatus { get; set; }

        public bool IsPensioner { get; set; }
        public bool IsReservist { get; set; }

        public double? MonthlyIncome { get; set; }
        public string Currency { get; set; }
        
        public static Client ConvertToDataSet(ClientObj clientObj,
            IEnumerable<Sex> sexes, IEnumerable<City> cities, IEnumerable<Disability> disabilities,
            IEnumerable<Nationality> nationalities, IEnumerable<FamilyStatus> familyStatuses, IEnumerable<Currency> currencies)
        {
            return new Client
            {
                ClientId = clientObj.ClientId,

                Surname = (clientObj.Surname),
                Name = (clientObj.Name),
                MiddleName = (clientObj.MiddleName),
                BirthDate = (clientObj.BirthDate),
                BirthPlace = (clientObj.BirthPlace),
                Sex = (clientObj.Sex),

                HomePhone = (clientObj.HomePhone),
                MobilePhone = (clientObj.MobilePhone),
                Email = (clientObj.Email),

                Passport = new Passport
                {
                    PassportSeries = clientObj.PassportSeries,
                    PassportNumber = clientObj.PassportNumber,
                    IdentNumber = clientObj.IdentNumber,
                    IssuedBy = clientObj.IssuedBy,
                    IssueDate = clientObj.IssueDate,
                },

                Residense = new Place
                {
                    City = cities.FirstOrDefault(x => x.Name == clientObj.ResidenseCity),
                    Adress = clientObj.ResidenseAdress,
                },

                Disability = disabilities.FirstOrDefault(x => x.Name == clientObj.Disability),
                Nationality = nationalities.FirstOrDefault(x => x.Name == clientObj.Nationality),
                FamilyStatus = familyStatuses.FirstOrDefault(x => x.Name == clientObj.FamilyStatus),
                IsPensioner = (clientObj.IsPensioner),
                IsReservist = (clientObj.IsReservist),
                MonthlyIncome = (clientObj.MonthlyIncome),
                Currency = currencies.FirstOrDefault(x => x.Name == clientObj.Currency),
            };
        }

        public static ClientObj ConvertToObj(Client client)
        {
            return new ClientObj
            {
                ClientId = client.ClientId,

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

                ResidenseCity = (client.Residense.City.Name),
                ResidenseAdress = (client.Residense.Adress),
                Disability = (client.Disability.Name),
                Nationality = (client.Nationality.Name),
                FamilyStatus = (client.FamilyStatus.Name),
                IsPensioner = (client.IsPensioner),
                IsReservist = (client.IsReservist),
                MonthlyIncome = (client.MonthlyIncome),
                Currency = (client.Currency.Name),
            };
        }
    }
}