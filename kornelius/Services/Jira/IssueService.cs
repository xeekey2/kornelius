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
        private static readonly string apiKey = "kasper110899";
        private static readonly string userName = "kasper";
        private static readonly string baseAddress = "http://itopgaver:8080/";

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

        public static async Task<Sprint> GetSprintsByBoardName(string boardName)
        {
            int? boardId = await GetBoardByName(boardName);
            if (!boardId.HasValue)
                return null;

            var response = await JiraApiWrapper.GetSprints(boardId.Value);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var sprints = JsonConvert.DeserializeObject<SprintResponse>(json);
                var newestSprint = sprints.values
                    .OrderByDescending(s => s.endDate)
                    .FirstOrDefault();

                return newestSprint;
            }
            else
            {
                return null;
            }
        }

        public static async Task<int?> GetBoardByName(string boardName)
        {
            var response = await JiraApiWrapper.GetBoardId(boardName);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var boardsResponse = JsonConvert.DeserializeObject<BoardResponse>(json);
                var board = boardsResponse.values.FirstOrDefault(b => b.name.Equals(boardName, StringComparison.OrdinalIgnoreCase));
                if (board != null)
                {
                    return board.id;
                }
            }
            return null;
        }

        public static async Task<List<Board>> GetBoards()
        {
            var response = await JiraApiWrapper.GetBoards();
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var boardsResponse = JsonConvert.DeserializeObject<BoardResponse>(json);
                return boardsResponse.values.ToList();
            }
            return null;
        }
    }
}

