using kornelius.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Services
{
    public static class IssueService
    {
        private static readonly string apiKey = "ATATT3xFfGF0MWSjJ8xm-cN8fTg1vCRs1F37kTXeppfq-1B-ACEq45hsQoBM9mj-OZUVfL_xXeuMeAmDJtw3uy4bd0ZRK82PSCCKqnZt1gCo2qKU0_PX1hODrvtsZNjAoX2zY-Wnn7ZiSYLzQhptFxZui1lgBqBTaOVovud_6MCOwx0PotdUL5c=E1885302";
        private static readonly string userName = "Kasper";
        private static readonly string baseAddress = "http://itopgaver:8080/";

        private static readonly JiraHttpClient JiraHttpClient = new JiraHttpClient(baseAddress, userName, apiKey);

        public static async Task<IEnumerable<Issue>> GetIssuesForSprintByAssignee(int sprintId, string assignee)
        {
            var jql = $"Sprint = {sprintId} AND assignee = {assignee}";
            var response = await JiraHttpClient.GetIssues(jql);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var jiraObject = JsonConvert.DeserializeObject<JiraObject>(json);
                return jiraObject.issues;
            }
            else
            {
                // Handle errors, log them, or throw exceptions as needed
                return Enumerable.Empty<Issue>();
            }
        }
    }

}
