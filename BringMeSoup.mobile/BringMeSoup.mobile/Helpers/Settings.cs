// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace BringMeSoup.mobile.Helpers
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


		public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }

        public static DateTime AccessTokenExpirationDate
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessTokenExpirationDate", DateTime.MinValue);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessTokenExpirationDate", value);
            }
        }

        public static string EmailAddress
        {
            get
            {
                return AppSettings.GetValueOrDefault("EmailAddress", string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue("EmailAddress", value);
            }
        }

        public static string SickCode
        {
            get
            {
                return AppSettings.GetValueOrDefault("SickCode", string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue("SickCode", value);
            }
        }

        public static string AssociatedSickUserId
        {
            get
            {
                return AppSettings.GetValueOrDefault("AssociatedSickUserId", string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AssociatedSickUserId", value);
            }
        }

        public static string AssociatedSickUserEmail
        {
            get
            {
                return AppSettings.GetValueOrDefault("AssociatedSickUserEmail", string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AssociatedSickUserEmail", value);
            }
        }

        public static void ClearAllValues()
        {
            AccessToken = string.Empty;
            AccessTokenExpirationDate = DateTime.MinValue;
            EmailAddress = string.Empty;
            SickCode = string.Empty;
            AssociatedSickUserId = string.Empty;
            AssociatedSickUserEmail = string.Empty;
        }

    }
}