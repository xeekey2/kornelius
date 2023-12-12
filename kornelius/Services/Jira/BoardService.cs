using kornelius.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Services.Jira
{
    public class BoardService
    {
        private static readonly string apiKey = "kasper110899";
        private static readonly string userName = "kasper";
        private static readonly string baseAddress = "http://itopgaver:8080/";

        private static readonly JiraApiWrapper JiraApiWrapper = new JiraApiWrapper(baseAddress, userName, apiKey);

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

        public static async Task<List<Board>> GetBoardsWithAssigneeIssues(string assignee)
        {
            var allBoardsResponse = await JiraApiWrapper.GetBoards();
            var allBoards = JsonConvert.DeserializeObject<BoardResponse>(await allBoardsResponse.Content.ReadAsStringAsync());
            var boardsWithAssigneeIssues = new List<Board>();

            foreach (var board in allBoards.values)
            {
                var issues = await IssueService.GetIssuesForBoardAndAssignee(board.id, assignee);
                if (issues.Any())
                {
                    boardsWithAssigneeIssues.Add(board);
                }
            }
            return boardsWithAssigneeIssues;
        }
    }
}
