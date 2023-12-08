using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using kornelius.Models;
using System.Data;
using LaserTryk.Util.Helpers;
using System.Configuration;
using System;
using System.Text;

public static class Jira
{

    public async static Task<List<string>> GetSprintsAsync()
    {
        List<string> sprints = new List<string>();
        StringBuilder stringBuilder = new StringBuilder(128);
        stringBuilder.Append("select NAME from AO_60DB71_SPRINT ");
        stringBuilder.Append("where dateadd(s, (start_date+7200000) / 1000, convert(datetime, '1-1-1970 00:00:00')) > DATEADD(month, -6, getdate()) ");
        stringBuilder.Append("order by START_DATE desc");
        DataTable dataTable = new DataTable();
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["lansql02"].ConnectionString))
        {
            SqlCommand sqlCommand = new SqlCommand(stringBuilder.ToString(), connection);
            connection.Open();
            dataTable.Load((IDataReader)sqlCommand.ExecuteReader());
        }
        foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            sprints.Add(DataRowHelper.GetString(row, "NAME"));
        return sprints;
    }


    public async static Task<List<Issue>> GetIssuesAsync(string assignee, string sprint)
    {
        List<Issue> issues = new List<Issue>();
        string connectionString = ConfigurationManager.ConnectionStrings["lansql02"].ConnectionString;
        StringBuilder stringBuilder = new StringBuilder(512);
        stringBuilder.Append("SELECT ji.[PROJECT], ji.[ASSIGNEE], ji.[issuetype], ji.[SUMMARY], ji.[DESCRIPTION], ji.[RESOLUTION], ji.[issuestatus], ji.[TIMEORIGINALESTIMATE], ji.[TIMEESTIMATE], ji.[TIMESPENT] ");
        stringBuilder.Append("from jiraissue ji ");
        stringBuilder.Append("join CustomFieldValue cfv on ji.id=cfv.issue ");
        stringBuilder.Append("join CustomField cf on cf.Id = cfv.CustomField ");
        stringBuilder.Append("join AO_60DB71_SPRINT s on cast(s.ID as nvarchar(50)) = cfv.stringvalue ");
        stringBuilder.Append("WHERE [ASSIGNEE] = @Assignee AND [RESOLUTION] is null AND cf.cfname='Sprint' and s.NAME=@Sprint ");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(stringBuilder.ToString(), connection))
            {
                command.Parameters.AddWithValue("@Assignee", assignee);
                command.Parameters.AddWithValue("@Sprint", sprint);

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            Issue issue = new Issue
                            {
                                Project = DataRowHelper.GetInt(row, "PROJECT"),
                                Assignee = DataRowHelper.GetString(row, "ASSIGNEE"),
                                IssueType = DataRowHelper.GetInt(row, "issuetype"),
                                Summary = DataRowHelper.GetString(row, "SUMMARY"),
                                Description = DataRowHelper.GetString(row, "DESCRIPTION"),
                                Resolution = DataRowHelper.GetString(row, "RESOLUTION"),
                                IssueStatus = DataRowHelper.GetString(row, "issuestatus"),
                                TimeOriginalEstimate = DataRowHelper.GetInt(row, "TIMEORIGINALESTIMATE"),
                                TimeEstimate = DataRowHelper.GetInt(row, "TIMEESTIMATE"),
                                TimeSpent = DataRowHelper.GetInt(row, "TIMESPENT")
                            };
                            issues.Add(issue);
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                    // Consider additional error handling
                }
            }
        }
        return issues;
    }
}
