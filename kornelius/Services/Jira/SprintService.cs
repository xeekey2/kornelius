using kornelius.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Services.Jira
{
    public class SprintService
    {
        private static readonly string apiKey = "kasper110899";
        private static readonly string userName = "kasper";
        private static readonly string baseAddress = "http://itopgaver:8080/";

        private static readonly JiraApiWrapper JiraApiWrapper = new JiraApiWrapper(baseAddress, userName, apiKey);

        public static async Task<Sprint> GetSprintsByBoardId(int boardId)
        {
            var response = await JiraApiWrapper.GetSprints(boardId);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var sprints = JsonConvert.DeserializeObject<SprintResponse>(json);
                var newestSprint = sprints.values.OrderByDescending(s => s.endDate).FirstOrDefault();
                return newestSprint;
            }
            else
            {
                return null;
            }
        }
    }
}
