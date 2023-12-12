using kornelius.Model;
using LaserTryk.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public class JiraApiWrapper
{
    private readonly HttpClient httpClient;

    public JiraApiWrapper(string baseAddress, string username, string apiToken)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseAddress),
        };
        var authToken = Encoding.ASCII.GetBytes($"{username}:{apiToken}");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
    }

    #region GET
    public async Task<HttpResponseMessage> GetIssues(string jql)
    {
        var builder = new UriBuilder($"{httpClient.BaseAddress}rest/api/2/search");
        var query = HttpUtility.ParseQueryString(builder.Query);
        query["jql"] = jql;
        query["maxResults"] = "100";
        builder.Query = query.ToString();
        string url = builder.ToString();
        return await httpClient.GetAsync(url);
    }

    public async Task<HttpResponseMessage> GetBoardIssues(int boardId, string assignee)
    {
        var builder = new UriBuilder($"{httpClient.BaseAddress}rest/agile/1.0/board/{boardId}/issue");
        var query = HttpUtility.ParseQueryString(builder.Query);
        query["jql"] = @$"assignee = {assignee} and status = 'In Progress'";
        query["maxResults"] = "100";
        builder.Query = query.ToString();
        string url = builder.ToString();
        return await httpClient.GetAsync(url);
    }

    public async Task<HttpResponseMessage> GetSprints(int boardId)
    {
        var builder = new UriBuilder($"{httpClient.BaseAddress}rest/agile/1.0/board/{boardId}/sprint");
        var query = HttpUtility.ParseQueryString(builder.Query);
        builder.Query = query.ToString();
        string url = builder.ToString();
        return await httpClient.GetAsync(url);
    }

    public async Task<HttpResponseMessage> GetBoardId(string boardName)
    {
        var builder = new UriBuilder($"{httpClient.BaseAddress}rest/agile/1.0/board?name={boardName}");
        var query = HttpUtility.ParseQueryString(builder.Query);
        builder.Query = query.ToString();
        string url = builder.ToString();
        return await httpClient.GetAsync(url);
    }

    public async Task<HttpResponseMessage> GetBoards()
    {
        var builder = new UriBuilder($"{httpClient.BaseAddress}rest/agile/latest/board");
        string url = builder.ToString();
        return await httpClient.GetAsync(url);
    }
    #endregion
}