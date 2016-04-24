using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace CodeFirst
{
    public class ClientRepository : SampleRepository
    {
        public ClientRepository(string dbConnection) : base(dbConnection)
        {
            RefreshLists();
        }

        private void RefreshLists()
        {
            Select<City>().Load();
            Select<Disability>().Load();
            Select<Nationality>().Load();
            Select<FamilyStatus>().Load();
            Select<Currency>().Load();
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

        public IEnumerable<string> GetNames<T>() where T : class, IName
        {
            return Select<T>().Local.Select(x => x.Name);
        } 

        public void UpdateOrAdd(ClientObj clientObj)
        {
            Client newClient = ClientObj.ConvertToDataSet(clientObj,
                (IEnumerable<Sex>)Enum.GetValues(typeof(Sex)),
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
                FillPlaces(origClient);
                Insert(clientObj);
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
            var finded = places.FirstOrDefault(x => 
                x.City.Name == origClient.Residense.City.Name && 
                x.Adress == origClient.Residense.Adress);
            if (finded != null)
            {
                origClient.ResidenseId = finded.PlaceId;
                origClient.Residense = finded;
            }
            finded = places.FirstOrDefault(x => 
                x.City.Name == origClient.Registration.City.Name && 
                x.Adress == origClient.Registration.Adress);
            if (finded != null)
            {
                origClient.RegistrationId = finded.PlaceId;
                origClient.Registration = finded;
            }
        }

        public void Delete(int clientId)
        {
            var removed = Select<Client>().FirstOrDefault(x => x.ClientId == clientId);
            Delete(removed);
        }
    }
}
