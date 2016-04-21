using System.Data.Common;
using System.Data.Entity;

namespace CodeFirst
{
    public class SampleContext : DbContext
    {
        static SampleContext()
        {
            Database.SetInitializer(new FillDefaultContextInitializer());
        }

        public SampleContext(string dbConnection)
            : base(dbConnection)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Passport> Passports { get; set; }

        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<FamilyStatus> FamilyStatuses { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Place> Places { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
