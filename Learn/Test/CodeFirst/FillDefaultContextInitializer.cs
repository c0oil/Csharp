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
            "�������",
            "�������",
            "����������",
            "�������",
            "�����",
            "��������",
            "���������",
            "�������",
            "�����",
            "�������",
            "����������",
            "��������",
        };

        readonly string[] cityNames =
        {
            "�����",
            "������",
            "����",
            "�������",
            "������",
            "�����",
            "��� ����",
        };

        readonly string[] disabilityNames =
        {
            "��� ������������",
            "������ 1",
            "������ 2",
            "������ 3",
        };

        readonly string[] familyStatusNames =
        {
            "�����",
            "�������",
            "�� �����",
            "�� �������",
        };

        readonly List<Client> clients = new List<Client>
        {
            new Client
            {
                Surname = "������",
                Name = "����",
                MiddleName = "������",
                BirthDate = new DateTime(1992, 9, 19),
                BirthPlace = "���",
                IsPensioner = true,
                IsReservist = true,
                MonthlyIncome = 90000,
                Sex = Sex.Male,
            },
            new Client
            {
                Surname = "�������",
                Name = "����",
                MiddleName = "�������",
                BirthDate = new DateTime(1995, 3, 10),
                BirthPlace = "���",
                IsPensioner = true,
                IsReservist = true,
                MonthlyIncome = 100000,
                Sex = Sex.Male,
            },
            new Client
            {
                Surname = "�������",
                Name = "�������",
                MiddleName = "������",
                BirthDate = new DateTime(1993, 5, 1),
                BirthPlace = "���",
                IsPensioner = true,
                IsReservist = true,
                MonthlyIncome = 80000,
                Sex = Sex.Male,
            },
            new Client
            {
                Surname = "���������",
                Name = "�����",
                MiddleName = "�������",
                BirthDate = new DateTime(1994, 9, 5),
                BirthPlace = "���",
                IsPensioner = true,
                IsReservist = false,
                MonthlyIncome = 70000,
                Sex = Sex.Female,
            },
            new Client
            {
                Surname = "������",
                Name = "������",
                MiddleName = "�����������",
                BirthDate = new DateTime(1996, 1, 8),
                BirthPlace = "���",
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
            var places = cities.Select(x => new Place { City = x, Adress = "��� �������"}).ToArray();
            
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