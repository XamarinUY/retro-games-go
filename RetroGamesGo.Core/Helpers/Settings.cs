using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace RetroGamesGo.Core.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting 
        private const string IdOnboardingShown = "show_onboarding";
        private static readonly bool OnboardingShownDefault = false;

        private const string IdFormCompleted = "form_completed";
        private static readonly bool FormCompletedDefault = false;

        private const string IdUrlBase = "url_base";
        private static readonly string UrlBaseDefault = string.Empty;
        #endregion

        public static bool OnboardingShown
        {
            get
            {
                return AppSettings.GetValueOrDefault(IdOnboardingShown, OnboardingShownDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IdOnboardingShown, value);
            }
        }

        public static bool FormCompleted
        {
            get
            {
                return AppSettings.GetValueOrDefault(IdFormCompleted, FormCompletedDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IdFormCompleted, value);
            }
        }

        public static string UrlBase
        {
            get
            {
                return AppSettings.GetValueOrDefault(IdUrlBase, UrlBaseDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IdUrlBase, value);
            }
        }

    }
}
