﻿using CommunityToolkit.Mvvm.ComponentModel;
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
                LoadSettings();
            }
        }

        partial void OnJiraUsernameChanged(string value) => SaveSettings();
        partial void OnJiraBaseUrlChanged(string value) => SaveSettings();
        partial void OnEnableAutomaticLoggingChanged(bool value) => SaveSettings();
        partial void OnShowEstimatedTimeChanged(bool value) => SaveSettings();
        partial void OnShowLoggedTimeChanged(bool value) => SaveSettings();
        partial void OnShowRemainingTimeChanged(bool value) => SaveSettings();

        private void LoadSettings()
        {
            var settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kornelius", "settings.json");

            if (File.Exists(settingsPath))
            {
                var json = File.ReadAllText(settingsPath);
                var settings = JsonConvert.DeserializeObject<Settings>(json);

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
        }

        private void SaveSettings()
        {
            EnsureDirectoryExists();

            var settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kornelius", "settings.json");

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
                File.WriteAllText(settingsPath, json);
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
