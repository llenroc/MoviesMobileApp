// Helpers/Settings.cs
using MoviesMobileApp.Service;
using MoviesMobileApp.Service.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MoviesMobileApp.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;

		#endregion

        public static ServiceConfiguration Configuration
        {
            get
            {
                var config = AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
                if (string.IsNullOrWhiteSpace(config))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<ServiceConfiguration>(config);
            }
            set
            {
                var convertedValue = JsonConvert.SerializeObject(value);
                AppSettings.AddOrUpdateValue(SettingsKey, convertedValue);
            }
        }
	}
}