using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace Test.DbConnection
{
    public static class ConnectionHelper
    {
        private const string DefaultConnectionString = "mainConnectionString";
        public static readonly SqlConnectionStringBuilder DefaultConnectionBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"C0_OIL-ой\SQLEXPRESS",
                //DataSource = @"DESKTOP-COMHVJF\SQLEXPRESS",
                InitialCatalog = "Default",
                MultipleActiveResultSets = true,
                IntegratedSecurity = true,
                ConnectTimeout = 30,
            };

        //Save only without Debuging
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

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringSettings setting = config.ConnectionStrings.ConnectionStrings[DefaultConnectionString];
            return setting == null
                ? DefaultConnectionBuilder.ConnectionString 
                : setting.ConnectionString;
        }

        public static bool TestConnection(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.ToString(), "Test connection", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
        }
    }
}