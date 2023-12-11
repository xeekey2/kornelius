using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Models
{
    public class Issue
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public Fields fields { get; set; }
    }
    public class Fields
    {
        public Issuetype issuetype { get; set; }
        public object[] components { get; set; }
        public int? timespent { get; set; }
        public int? timeoriginalestimate { get; set; }
        public string description { get; set; }
        public Project project { get; set; }
        public int? aggregatetimespent { get; set; }
        public Resolution resolution { get; set; }
        public string customfield_10005 { get; set; }
        public object customfield_10401 { get; set; }
        public object customfield_10402 { get; set; }
        public string customfield_10009 { get; set; }
        public int? aggregatetimeestimate { get; set; }
        public DateTime? resolutiondate { get; set; }
        public int? workratio { get; set; }
        public string summary { get; set; }
        public object lastViewed { get; set; }
        public Watches watches { get; set; }
        public Creator creator { get; set; }
        public object[] subtasks { get; set; }
        public DateTime created { get; set; }
        public Reporter reporter { get; set; }
        public Customfield_10000[] customfield_10000 { get; set; }
        public Aggregateprogress aggregateprogress { get; set; }
        public Priority priority { get; set; }
        public object customfield_10001 { get; set; }
        public string customfield_10100 { get; set; }
        public object customfield_10200 { get; set; }
        public string[] labels { get; set; }
        public string[] customfield_10004 { get; set; }
        public object customfield_10400 { get; set; }
        public object environment { get; set; }
        public int? timeestimate { get; set; }
        public int? aggregatetimeoriginalestimate { get; set; }
        public string duedate { get; set; }
        public Progress progress { get; set; }
        public object[] issuelinks { get; set; }
        public Votes votes { get; set; }
        public Assignee assignee { get; set; }
        public DateTime updated { get; set; }
        public Status status { get; set; }
        public override string ToString()
        {
            return summary;
        }
    }

    public class IssueResponse
    {
        public string expand { get; set; }
        public int startAt { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public Issue[] issues { get; set; }
    }


    public class Issuetype
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public bool subtask { get; set; }
        public int avatarId { get; set; }
    }

    public class Project
    {
        public string self { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public Avatarurls avatarUrls { get; set; }
        public Projectcategory projectCategory { get; set; }
    }

    public class Avatarurls
    {
        public string _48x48 { get; set; }
        public string _24x24 { get; set; }
        public string _16x16 { get; set; }
        public string _32x32 { get; set; }
    }

    public class Projectcategory
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }

    public class Resolution
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }

    public class Watches
    {
        public string self { get; set; }
        public int watchCount { get; set; }
        public bool isWatching { get; set; }
    }

    public class Creator
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string emailAddress { get; set; }
        public Avatarurls1 avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    public class Avatarurls1
    {
        public string _48x48 { get; set; }
        public string _24x24 { get; set; }
        public string _16x16 { get; set; }
        public string _32x32 { get; set; }
    }

    public class Reporter
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string emailAddress { get; set; }
        public Avatarurls2 avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    public class Avatarurls2
    {
        public string _48x48 { get; set; }
        public string _24x24 { get; set; }
        public string _16x16 { get; set; }
        public string _32x32 { get; set; }
    }

    public class Aggregateprogress
    {
        public int progress { get; set; }
        public int total { get; set; }
        public int percent { get; set; }
    }

    public class Priority
    {
        public string self { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Progress
    {
        public int progress { get; set; }
        public int total { get; set; }
        public int percent { get; set; }
    }

    public class Votes
    {
        public string self { get; set; }
        public int votes { get; set; }
        public bool hasVoted { get; set; }
    }

    public class Assignee
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string emailAddress { get; set; }
        public Avatarurls3 avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    public class Avatarurls3
    {
        public string _48x48 { get; set; }
        public string _24x24 { get; set; }
        public string _16x16 { get; set; }
        public string _32x32 { get; set; }
    }

    public class Status
    {
        public string self { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public Statuscategory statusCategory { get; set; }
    }

    public class Statuscategory
    {
        public string self { get; set; }
        public int id { get; set; }
        public string key { get; set; }
        public string colorName { get; set; }
        public string name { get; set; }
    }

    public class Customfield_10000
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

}
