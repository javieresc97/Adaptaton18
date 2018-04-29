// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AdaptatonMobile.Helpers
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

        public static bool IsUserSet => AppSettings.Contains(nameof(UserId));
        private static readonly string StringDefault = string.Empty;

		#endregion


        public static string UserId
        {
            get => AppSettings.GetValueOrDefault(nameof(UserId), StringDefault);
            set => AppSettings.AddOrUpdateValue(nameof(UserId), value);
        }

        public static void ClearAll()
        {
            AppSettings.Clear();
        }
	}
}