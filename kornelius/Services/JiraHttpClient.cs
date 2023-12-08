using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public class JiraHttpClient
{
    private readonly HttpClient httpClient;

    public JiraHttpClient(string baseAddress, string username, string apiToken)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseAddress),
        };

    }

    public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
    {
        return await httpClient.PostAsync(uri, content);
    }

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
}