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
        public static async Task<Settings> GetSettings()
        {
            var settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kornelius", "settings.json");
            if (File.Exists(settingsPath))
            {
                var json = await File.ReadAllTextAsync(settingsPath);
                return JsonConvert.DeserializeObject<Settings>(json);
            }
            return null;
        }

        public static async Task SaveSettings(Settings setting)
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
            await File.WriteAllTextAsync(settingsPath, json);
        }

        private static async Task EnsureDirectoryExists()
        {
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kornelius");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

    }
}
