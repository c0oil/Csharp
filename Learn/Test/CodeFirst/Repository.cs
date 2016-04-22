using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace CodeFirst
{
    public class SampleRepository
    {
        protected readonly ClientContext context;

        public SampleRepository(string dbConnection)
        {
            context = new ClientContext(dbConnection);
            context.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));
        }

        public DbSet<TEntity> Select<TEntity>()
            where TEntity : class
        {
            return context.Set<TEntity>();
        }

        public ObservableCollection<TEntity> SelectLocal<TEntity>()
            where TEntity : class
        {
            return context.Set<TEntity>().Local;
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
        }
        
        public void Inserts<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            
            foreach (TEntity entity in entities)
                context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();

            context.Configuration.AutoDetectChangesEnabled = true;
            context.Configuration.ValidateOnSaveEnabled = true;
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();
        }
    }
}
