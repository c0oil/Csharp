using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace CodeFirst
{
    /*public class ListRepository : SampleRepository
    {
        public ListRepository(string dbConnection) : base(dbConnection)
        {
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
                Delete(removed);
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
    }*/
/*
    public enum ClientList
    {
        City,
        Disability,
        Nationality,
        FamilyStatus,
        Currency,
    }*/
}
