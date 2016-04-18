using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq.Expressions;
using CodeFirst;

namespace CodeFirst
{
    public class SampleContext : DbContext
    {
        static SampleContext()
        {
            Database.SetInitializer(new FillDefaultContextInitializer());
        }

        public SampleContext(DbConnection dbConnection)
            : base(dbConnection, false)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Passport> Passports { get; set; }

        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<FamilyStatus> FamilyStatuses { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Residense> Residenses { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
