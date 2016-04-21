using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CodeFirst
{
    public class FillDefaultContextInitializer : CreateDatabaseIfNotExists<SampleContext>
    //public class FillDefaultContextInitializer : DropCreateDatabaseIfModelChanges<SampleContext>
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
            "Белорус",
            "Русский",
            "Аргентинец",
            "Француз",
            "Немец",
            "Датчанин",
            "Итальянец",
            "Латвиец",
            "Поляк",
            "Испанец",
            "Американец",
            "Украинец",
        };

        readonly string[] cityNames =
        {
            "Минск",
            "Москва",
            "Киев",
            "Вильнюс",
            "Краков",
            "Париж",
            "Нью Йорк",
        };

        readonly string[] disabilityNames =
        {
            "Без инвалидности",
            "Группа 1",
            "Группа 2",
            "Группа 3",
        };

        readonly string[] familyStatusNames =
        {
            "Женат",
            "Замужем",
            "Не женат",
            "Не замужем",
        };

        readonly List<Client> clients = new List<Client>
        {
            new Client
            {
                Surname = "Пупкин",
                Name = "Вася",
                MiddleName = "Папкин",
                BirthDate = new DateTime(1992, 9, 19),
                BirthPlace = "Дом",
                IsPensioner = true,
                IsReservist = true,
                MonthlyIncome = 90000,
                Sex = Sex.Male,
            },
            new Client
            {
                Surname = "Васькин",
                Name = "Вася",
                MiddleName = "Васькин",
                BirthDate = new DateTime(1995, 3, 10),
                BirthPlace = "Дом",
                IsPensioner = true,
                IsReservist = true,
                MonthlyIncome = 100000,
                Sex = Sex.Male,
            },
            new Client
            {
                Surname = "Сорокин",
                Name = "Антонио",
                MiddleName = "Мамкин",
                BirthDate = new DateTime(1993, 5, 1),
                BirthPlace = "Дом",
                IsPensioner = true,
                IsReservist = true,
                MonthlyIncome = 80000,
                Sex = Sex.Male,
            },
            new Client
            {
                Surname = "Нисорокин",
                Name = "Димас",
                MiddleName = "Браткин",
                BirthDate = new DateTime(1994, 9, 5),
                BirthPlace = "Дом",
                IsPensioner = true,
                IsReservist = false,
                MonthlyIncome = 70000,
                Sex = Sex.Female,
            },
            new Client
            {
                Surname = "Пушкин",
                Name = "Серега",
                MiddleName = "Анатольевич",
                BirthDate = new DateTime(1996, 1, 8),
                BirthPlace = "Дом",
                IsPensioner = true,
                IsReservist = false,
                MonthlyIncome = 50000,
                Sex = Sex.Male,
            }
        };

        #endregion

        protected override void Seed(SampleContext context)
        {
            FillDefaultData(context);
            base.Seed(context);
        }

        private void FillDefaultData(SampleContext context)
        {
            var currencyies = currencyNames.Select(x => new Currency {CurrencyId = x}).ToArray();
            var nationalities = nationalityNames.Select(x => new Nationality {NationalityId = x}).ToArray();
            var disabilities = disabilityNames.Select(x => new Disability { DisabilityId = x }).ToArray();
            var familyStatuses = familyStatusNames.Select(x => new FamilyStatus { FamilyStatusId = x }).ToArray();
            var cities = cityNames.Select(x => new City { CityId = x }).ToArray();
            var places = cities.Select(x => new Place { City = x, Adress = "Дом пушкина"}).ToArray();
            
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
                    IssuedBy = "Belarusia", PassportSeries = "AB", PassportNumber = "123456", IdentNumber = "1111111aaaa"
                };
                passports.Add(passport);

                client.Passport = passport;
                client.Residense = TakeItem(i, 3, places);
                client.Registration = TakeItem(i, 2, places);
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