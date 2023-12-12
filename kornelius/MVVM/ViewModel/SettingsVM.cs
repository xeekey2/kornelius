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

    public partial class SettingsVM : VM
    {
        [ObservableProperty]
        private string jiraUsername;

        [ObservableProperty]
        private string jiraBaseUrl = "http://itopgaver:8080";

        [ObservableProperty]
        private bool enableAutomaticLogging;

        [ObservableProperty]
        private bool showEstimatedTime;

        [ObservableProperty]
        private bool showLoggedTime;

        [ObservableProperty]
        private bool showRemainingTime;

        public SettingsVM()
        {
            // Constructor logic if necessary
        }

        partial void OnJiraUsernameChanged(string value) => SaveSettings();
        partial void OnJiraBaseUrlChanged(string value) => SaveSettings();
        partial void OnEnableAutomaticLoggingChanged(bool value) => SaveSettings();
        partial void OnShowEstimatedTimeChanged(bool value) => SaveSettings();
        partial void OnShowLoggedTimeChanged(bool value) => SaveSettings();
        partial void OnShowRemainingTimeChanged(bool value) => SaveSettings();

        private void SaveSettings()
        {
            var settings = new
            {
                JiraUsername,
                JiraBaseUrl,
                EnableAutomaticLogging,
                ShowEstimatedTime,
                ShowLoggedTime,
                ShowRemainingTime
            };

            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText("settings.json", json);
        }
    }

}
