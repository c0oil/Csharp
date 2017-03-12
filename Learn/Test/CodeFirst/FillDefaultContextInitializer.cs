using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CodeFirst
{
    //public class FillDefaultContextInitializer : CreateDatabaseIfNotExists<ClientContext>
    public class FillDefaultContextInitializer : DropCreateDatabaseIfModelChanges<ClientContext>
    //public class FillDefaultContextInitializer : DropCreateDatabaseAlways<ClientContext>
    {
        #region Default data

        readonly string[] currencyNames =
        {
            "USD",
            "RUS",
            "BLR",
        };

        readonly string[] nationalityNames =
        {
            "belarus",
            "russian",
            "american",
            "french",
            "polac",
            "litvinian",
            "ukranian",
        };

        readonly string[] cityNames =
        {
            "Minsk",
            "Nepal",
            "Moscow",
            "New York",
            "Paris",
            "Madrid",
            "Praha",
        };

        readonly string[] disabilityNames =
        {
            "No disability",
            "Stage 1",
            "Stage 2",
            "Stage 3",
        };

        readonly string[] familyStatusNames =
        {
            "Alone",
            "Meet",
            "No women",
            "No men",
        };

        readonly List<Client> clients = new List<Client>
        {
            new Client
            {
                Surname = "John",
                Name = "John",
                MiddleName = "John",
                BirthDate = new DateTime(1992, 9, 19),
                BirthPlace = "Дом",
                IsPensioner = true,
                IsReservist = true,
                MonthlyIncome = 90000,
                Sex = Sex.Male,
            },
            new Client
            {
                Surname = "Rick",
                Name = "Ross",
                MiddleName = "Deniel",
                BirthDate = new DateTime(1995, 3, 10),
                BirthPlace = "Дом",
                IsPensioner = true,
                IsReservist = true,
                MonthlyIncome = 100000,
                Sex = Sex.Male,
            },
            new Client
            {
                Surname = "Rocky",
                Name = "Balboa",
                MiddleName = "Stalone",
                BirthDate = new DateTime(1993, 5, 1),
                BirthPlace = "Дом",
                IsPensioner = true,
                IsReservist = true,
                MonthlyIncome = 80000,
                Sex = Sex.Male,
            },
            new Client
            {
                Surname = "Eddi",
                Name = "Merhi",
                MiddleName = "Good",
                BirthDate = new DateTime(1994, 9, 5),
                BirthPlace = "Дом",
                IsPensioner = true,
                IsReservist = false,
                MonthlyIncome = 70000,
                Sex = Sex.Female,
            },
            new Client
            {
                Surname = "Julian",
                Name = "Lassanj",
                MiddleName = "Google",
                BirthDate = new DateTime(1996, 1, 8),
                BirthPlace = "Дом",
                IsPensioner = true,
                IsReservist = false,
                MonthlyIncome = 50000,
                Sex = Sex.Male,
            }
        };

        #endregion

        protected override void Seed(ClientContext context)
        {
            FillDefaultData(context);
            base.Seed(context);
        }

        private void FillDefaultData(ClientContext context)
        {
            var currencyies = currencyNames.Select(x => new Currency { Name = x }).ToArray();
            var nationalities = nationalityNames.Select(x => new Nationality { Name = x }).ToArray();
            var disabilities = disabilityNames.Select(x => new Disability { Name = x }).ToArray();
            var familyStatuses = familyStatusNames.Select(x => new FamilyStatus { Name = x }).ToArray();
            var cities = cityNames.Select(x => new City { Name = x }).ToArray();
            var places = cities.Select(x => new Place { City = x, Adress = "Ул. Казинца"}).ToArray();
            
            context.Cities.AddRange(cities);
            context.Disabilities.AddRange(disabilities);
            context.FamilyStatuses.AddRange(familyStatuses);
            context.Nationalities.AddRange(nationalities);
            context.Currencies.AddRange(currencyies);
            context.Places.AddRange(places);

            var passports = new List<Passport>();
            for (int i = 0; i < clients.Count; i++)
            {
                var client = clients[i];
                var passport = new Passport
                {
                    Client = client, IssueDate = new DateTime(1996, 1, 8),
                    IssuedBy = "Налоговой",
                    PassportSeries = "AB",
                    PassportNumber = "AB1234567",
                    IdentNumber = "1234567890abCD"
                };
                passports.Add(passport);

                client.Passport = passport;
                client.Residense = TakeItem(i, 3, places);
                client.Disability = TakeItem(i, 3, disabilities);
                client.Nationality = TakeItem(i, 4, nationalities);
                client.FamilyStatus = TakeItem(i, 3, familyStatuses);
                client.Currency = TakeItem(i, 2, currencyies);
            }

            context.Passports.AddRange(passports);
            context.Clients.AddRange(clients);
            context.SaveChanges();
        }

        private T TakeItem<T>(int index, int mul, IEnumerable<T> array)
        {
            return array.ElementAt(index * mul % array.Count());
        }
    }
}