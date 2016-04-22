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
                MessageBox.Show("Please, input correct data", "UpdateOrAdd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Client origClient = Select<Client>().Find(newClient.ClientId);
            if (origClient == null)
            {
                Insert(clientObj);
                MessageBox.Show("Data insered", "UpdateOrAdd", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                newClient.CopyTo(origClient);
                Update(origClient);
                MessageBox.Show("Data updated", "UpdateOrAdd", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void Delete(int clientId)
        {
            var removed = Select<Client>().FirstOrDefault(x => x.ClientId == clientId);
            Delete(removed);
        }
    }
}
