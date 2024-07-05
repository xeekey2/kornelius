using kornelius.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Services.Jira
{
    public static class LogService
    {
        private static readonly string apiKey = "kasper110899";
        private static readonly string userName = "kasper";
        private static readonly string baseAddress = "http://itopgaver:8080/";
        private static readonly string assignee = "kasper";


        private static readonly JiraApiWrapper JiraApiWrapper = new JiraApiWrapper(baseAddress, userName, apiKey);

        public static async Task LogTimeOnIssue(string issueKey, string timeSpent, string comment)
        {
            var response = await JiraApiWrapper.LogTimeOnIssue(issueKey, timeSpent, comment);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
