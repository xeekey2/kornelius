using kornelius.MVVM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Services.Setting
{
    public static class SettingService
    {
        public static Settings LoadSettings()
        {
            var settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kornelius", "settings.json");
            if (File.Exists(settingsPath))
            {
                var json = File.ReadAllText(settingsPath);
                return JsonConvert.DeserializeObject<Settings>(json);
            }
            return null;
        }

        public static void SaveSettings(Settings setting)
        {
            EnsureDirectoryExists();

            var settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kornelius", "settings.json");

            var settings = new
            {
                setting.JiraUsername,
                setting.JiraBaseUrl,
                setting.EnableAutomaticLogging,
                setting.ShowEstimatedTime,
                setting.ShowLoggedTime,
                setting.ShowRemainingTime
            };

            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(settingsPath, json);
        }

        private static void EnsureDirectoryExists()
        {
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kornelius");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

    }
}
