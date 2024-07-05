using CommunityToolkit.Mvvm.ComponentModel;
using kornelius.Model;
using kornelius.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.ViewModel
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System.IO;
    using Newtonsoft.Json;
    using kornelius.Services.Setting;

    public partial class SettingsVM : VM
    {
        [ObservableProperty] private string jiraUsername;
        [ObservableProperty] private string jiraBaseUrl = "http://itopgaver:8080";
        [ObservableProperty] private bool enableAutomaticLogging;
        [ObservableProperty] private bool showEstimatedTime;
        [ObservableProperty] private bool showLoggedTime;
        [ObservableProperty] private bool showRemainingTime;

        private bool IsSettingsLoaded = false;

        public SettingsVM()
        {
            if (!IsSettingsLoaded)
            {
                GetSettings();

            }
        }

        partial void OnJiraUsernameChanged(string value) => SaveSettings();
        partial void OnJiraBaseUrlChanged(string value) => SaveSettings();
        partial void OnEnableAutomaticLoggingChanged(bool value) => SaveSettings();
        partial void OnShowEstimatedTimeChanged(bool value) => SaveSettings();
        partial void OnShowLoggedTimeChanged(bool value) => SaveSettings();
        partial void OnShowRemainingTimeChanged(bool value) => SaveSettings();


        private async void GetSettings()
        {
            var settings = await SettingService.GetSettings();
            if (settings != null)
            {
                JiraUsername = settings.JiraUsername;
                JiraBaseUrl = settings.JiraBaseUrl;
                EnableAutomaticLogging = settings.EnableAutomaticLogging;
                ShowEstimatedTime = settings.ShowEstimatedTime;
                ShowLoggedTime = settings.ShowLoggedTime;
                ShowRemainingTime = settings.ShowRemainingTime;
                IsSettingsLoaded = true;
            }
        }

        private async void SaveSettings()
        {
            var settings = new Settings
            {
                JiraUsername = JiraUsername,
                JiraBaseUrl = JiraBaseUrl,
                EnableAutomaticLogging = EnableAutomaticLogging,
                ShowEstimatedTime = ShowEstimatedTime,
                ShowLoggedTime = ShowLoggedTime,
                ShowRemainingTime = ShowRemainingTime
            };
            await SettingService.SaveSettings(settings);
        }


        private void EnsureDirectoryExists()
        {
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kornelius");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }

}
