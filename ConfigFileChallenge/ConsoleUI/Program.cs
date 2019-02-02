using System;
using System.Configuration;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadSetting("UserName");
            Console.WriteLine();
            ReadAllSettings();
            Console.WriteLine();
            ReadAllConnectionStrings();
            Console.WriteLine();
            Console.WriteLine("Changing Username value");
            Console.WriteLine();
            ChangeSettingValue("UserName", "Tiago");

            ReadSetting("UserName");

            Console.ReadLine();
        }

        private static void ChangeSettingValue(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var settings = configFile.AppSettings.Settings;

                if(settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch(ConfigurationErrorsException)
            {

                Console.WriteLine("Error writting app settings");
            }
        }

        private static void ReadAllConnectionStrings()
        {
            var conStrings = ConfigurationManager.ConnectionStrings;
            Console.WriteLine();

            foreach(ConnectionStringSettings key in conStrings)
            {
                Console.WriteLine(key.ConnectionString);
            }
        }

        private static void ReadConnectionString(string key)
        {

            Console.WriteLine(ConfigurationManager.ConnectionStrings[key].ConnectionString);
        }

        private static void ReadSetting(string key)
        {
            try
            {
                var appSetting = ConfigurationManager.AppSettings;

                string result = appSetting[key] ?? "Not Found";
                Console.WriteLine($"The {key} value is {result}");
            }
            catch(ConfigurationErrorsException)
            {

                throw;
            }
        }

        static void ReadAllSettings()
        {
            Console.WriteLine("Settings Section");
            Console.WriteLine();
            var appSetting = ConfigurationManager.AppSettings;

            foreach(var item in appSetting.AllKeys)
            {
                Console.WriteLine($"Key: {item} / Value: {appSetting[item]} ");
            }
            Console.WriteLine();
        }
    }
}
