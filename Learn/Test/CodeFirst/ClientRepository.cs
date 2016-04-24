using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace CodeFirst
{
    public class ClientRepository : SampleRepository
    {
        public ClientRepository(string dbConnection) : base(dbConnection)
        {
        }

        public IEnumerable<Place> GetTest()
        {
            return Select<Place>().Include(x => x.City);
        }

        public IEnumerable<ClientObj> GetAll()
        {
            return Select<Client>().
                Include(x => x.Passport).
                Include(x => x.Currency).
                Include(x => x.FamilyStatus).
                Include(x => x.Disability).
                Include(x => x.Nationality).
                Include(x => x.Residense).
                Include(x => x.Residense.City).
                Include(x => x.Registration).
                Include(x => x.Registration.City).
                Select(ClientObj.ConvertToObj).AsEnumerable();
        }

        public void UpdateOrAdd(ClientObj clientObj)
        {
            Client newClient = ClientObj.ConvertToDataSet(clientObj, 
                (IEnumerable<Sex>) Enum.GetValues(typeof (Sex)), 
                SelectLocal<City>(), 
                SelectLocal<Disability>(), 
                SelectLocal<Nationality>(), 
                SelectLocal<FamilyStatus>(), 
                SelectLocal<Currency>());
            
            if (newClient.NotValid())
            {
                MessageBox.Show("Please, input correct data", "ClientRepository", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Client origClient = Select<Client>().Find(newClient.ClientId);
            if (origClient == null)
            {
                FillPlaces(newClient);
                Select<Client>().Add(newClient);
                context.SaveChanges();
                //Insert(clientObj);
                MessageBox.Show("Data insered", "ClientRepository", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                newClient.CopyTo(origClient);
                FillPlaces(origClient);
                Update(origClient);
                MessageBox.Show("Data updated", "ClientRepository", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void FillPlaces(Client origClient)
        {
            IQueryable<Place> places = Select<Place>().Include(x => x.City);
            var finded = places.FirstOrDefault(x => x.City.Name == origClient.Residense.City.Name && x.Adress == origClient.Residense.Adress);
            if (finded != null)
            {
                origClient.ResidenseId = finded.PlaceId;
                origClient.Residense = finded;
            }
            finded = places.FirstOrDefault(x => x.City.Name == origClient.Registration.City.Name && x.Adress == origClient.Registration.Adress);
            if (finded != null)
            {
                origClient.RegistrationId = finded.PlaceId;
                origClient.Registration = finded;
            }
        }

        public void Delete(int clientId)
        {
            Client removed = Select<Client>().FirstOrDefault(x => x.ClientId == clientId);
            if (removed != null)
            {
                Delete(removed);
            }
        }

        public void RefreshLists()
        {
            Select<City>().Load();
            Select<Disability>().Load();
            Select<Nationality>().Load();
            Select<FamilyStatus>().Load();
            Select<Currency>().Load();
            Select<Place>().Load();
        }

        public IEnumerable<string> GetNames<T>() where T : class, IEntityList
        {
            return Select<T>().Local.Select(x => x.Name);
        }

        public IEntityList GetNewListEntity(ClientList selectedList)
        {
            switch (selectedList)
            {
                case ClientList.City:
                    return new City();
                case ClientList.Disability:
                    return new Disability();
                case ClientList.Nationality:
                    return new Nationality();
                case ClientList.FamilyStatus:
                    return new FamilyStatus();
                case ClientList.Currency:
                    return new Currency();
                default:
                    throw new ArgumentOutOfRangeException("selectedList", selectedList, null);
            }
        }

        public IEnumerable<IEntityList> GetList(ClientList selectedList)
        {
            switch (selectedList)
            {
                case ClientList.City:
                    return context.Cities;
                case ClientList.Disability:
                    return context.Disabilities;
                case ClientList.Nationality:
                    return context.Nationalities;
                case ClientList.FamilyStatus:
                    return context.FamilyStatuses;
                case ClientList.Currency:
                    return context.Currencies;
                default:
                    throw new ArgumentOutOfRangeException("selectedList", selectedList, null);
            }
        }

        public void Delete(int entityId, ClientList selectedList)
        {
            var list = GetList(selectedList);
            IEntityList removed = list.FirstOrDefault(x => x.Id == entityId);
            if (removed != null)
            {
                switch (selectedList)
                {
                    case ClientList.City:
                        context.Cities.Remove((City) removed);
                        break;
                    case ClientList.Disability:
                        context.Disabilities.Remove((Disability)removed);
                        break;
                    case ClientList.Nationality:
                        context.Nationalities.Remove((Nationality)removed);
                        break;
                    case ClientList.FamilyStatus:
                        context.FamilyStatuses.Remove((FamilyStatus)removed);
                        break;
                    case ClientList.Currency:
                        context.Currencies.Remove((Currency)removed);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("selectedList", selectedList, null);
                }
                context.SaveChanges();
                //Delete(removed);
            }
        }

        public void Add(string newName, ClientList selectedList)
        {
            IEnumerable<IEntityList> list = GetList(selectedList);
            IEntityList finded = list.FirstOrDefault(x => x.Name == newName);
            if (finded != null)
            {
                MessageBox.Show("This name contains yet", "listRepository", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            IEntityList newEntity = GetNewListEntity(selectedList);
            newEntity.Name = newName;
            Insert(newEntity);
        }

        public void Update(int entityId, string newName, ClientList selectedList)
        {
            IEnumerable<IEntityList> list = GetList(selectedList);
            IEntityList finded = list.FirstOrDefault(x => x.Name == newName);
            if (finded != null)
            {
                MessageBox.Show("This name contains yet", "listRepository", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            finded = list.FirstOrDefault(x => x.Id == entityId);
            if (finded != null)
            {
                finded.Name = newName;
                Update(finded);
            }
        }
    }

    public enum ClientList
    {
        City,
        Disability,
        Nationality,
        FamilyStatus,
        Currency,
    }
}
