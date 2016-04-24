using System.Configuration;
using System.Data.SqlClient;

namespace Test.DbConnection
{
    public static class ConnectionHelper
    {
        private const string DefaultConnectionString = "mainConnectionString";

        public static void SaveConnectionStringSettings(string connectionString)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringSettings setting = config.ConnectionStrings.ConnectionStrings[DefaultConnectionString];
            if (setting == null)
            {
                config.ConnectionStrings.ConnectionStrings.Add(
                    new ConnectionStringSettings(DefaultConnectionString,connectionString));
            }
            else
            {
                config.ConnectionStrings.ConnectionStrings[DefaultConnectionString].ConnectionString = connectionString;
            }
            
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        public static string GetConnectionStringSettings()
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = @"C0_OIL-ой\SQLEXPRESS",
                //DataSource = @"DESKTOP-COMHVJF\SQLEXPRESS",
                InitialCatalog = "Default",
                MultipleActiveResultSets = true,
                IntegratedSecurity = true,
                ConnectTimeout = 30,
            };

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringSettings setting = config.ConnectionStrings.ConnectionStrings[DefaultConnectionString];
            return setting == null 
                ? connectionString.ConnectionString 
                : setting.ConnectionString;
        }
    }
}