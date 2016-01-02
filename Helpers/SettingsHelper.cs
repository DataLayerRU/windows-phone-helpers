using System;
using System.IO.IsolatedStorage;

namespace WPDevelopment.Helpers
{
    public class SettingsHelper
    {
        private static IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public static T GetApplicationSetting<T>(string _key, T _default)
        {
            T result = _default;

            try
            {
                if (settings.Contains(_key))
                {
                    result = (T)settings[_key];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public static void AddApplicationSettings(string _key, object _value)
        {
            try
            {
                if (!settings.Contains(_key))
                {
                    settings.Add(_key, _value);
                }
            }
            catch (IsolatedStorageException ex)
            {
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    Int64 sizeNeeded = 1024 * 1024 * 100;
                    if (sizeNeeded > isf.AvailableFreeSpace)
                    {
                        Int64 newValue = isf.Quota - isf.AvailableFreeSpace + sizeNeeded;
                        isf.IncreaseQuotaTo(newValue);
                    }
                }
            }
            finally
            {
                settings[_key] = _value;
                settings.Save();
            }
        }

        public static void RemoveApplicationSetting(string _key)
        {
            try
            {
                settings.Remove(_key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
