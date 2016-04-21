using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    public class SampleRepository
    {
        private string dbConnection;
        private readonly SampleContext context;

        public SampleRepository(string dbConnection)
        {
            context = new SampleContext(dbConnection);
            context.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));
        }

        public DbSet<TEntity> Select<TEntity>()
            where TEntity : class
        {
            return context.Set<TEntity>();
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
    }
}
