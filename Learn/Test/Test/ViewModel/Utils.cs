using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace Test.ViewModel
{
    public static class UiHelper
    {
        public static Window FindLastActiveWindow()
        {
            return Application.Current.Windows.Cast<Window>().LastOrDefault(window => window.IsActive);
        }

        public static void HidePreviousWindow(Window formToShow, Window formToHide)
        {
            if (formToHide != null && formToHide != formToShow)
            {
                formToShow.Loaded += (s, e) => formToHide.Visibility = Visibility.Collapsed;
                formToShow.Closed += (s, e) => formToHide.Visibility = Visibility.Visible;
            }
        }
    }
    
    //Save only without Debuging
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
                DataSource = @"C0_OIL-ПК\SQLEXPRESS",
                //DataSource = @"DESKTOP-COMHVJF\SQLEXPRESS",
                InitialCatalog = "Default",
                MultipleActiveResultSets = true,
                IntegratedSecurity = true,
            };

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringSettings setting = config.ConnectionStrings.ConnectionStrings[DefaultConnectionString];
            return setting == null 
                ? connectionString.ConnectionString 
                : setting.ConnectionString;
        }
    }

    public static class EnumHelper
    {
        public static string GetDescription(this Enum enumVal)
        {
            return GetAttributeOfType<DescriptionAttribute>(enumVal).Description;
        }

        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}
