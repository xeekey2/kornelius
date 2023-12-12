using kornelius.Model;
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
        private static readonly string apiKey = "kasper110899";
        private static readonly string userName = "kasper";
        private static readonly string baseAddress = "http://itopgaver:8080/";
        private static readonly string assignee = "kasper";


        private static readonly JiraApiWrapper JiraApiWrapper = new JiraApiWrapper(baseAddress, userName, apiKey);

        public static async Task<IEnumerable<Issue>> GetIssuesForSprintByAssignee(int sprintId, string assignee)
        {
            var jql = $"Sprint = {sprintId} AND assignee = {assignee}";
            var response = await JiraApiWrapper.GetIssues(jql);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var issueResponse = JsonConvert.DeserializeObject<IssueResponse>(json);
                return issueResponse.issues;
            }
            else
            {
                return Enumerable.Empty<Issue>();
            }
        }

        public static async Task<IEnumerable<Issue>> GetIssuesForBoardAndAssignee(int boardId, string assignee)
        {
            var response = await JiraApiWrapper.GetBoardIssues(boardId, assignee);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var issueResponse = JsonConvert.DeserializeObject<IssueResponse>(json);
                return issueResponse.issues;
            }
            else
            {
                return Enumerable.Empty<Issue>();
            }
        }
    }
}

