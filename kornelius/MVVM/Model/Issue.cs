using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Model
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
        public int? timespent { get; set; }
        public int? timeoriginalestimate { get; set; }
        public string description { get; set; }
        public int? aggregatetimespent { get; set; }
        public double? AggregateTimeSpentHours
        {
            get
            {
                if (aggregatetimespent.HasValue)
                {
                    // Convert seconds to hours
                    return aggregatetimespent.Value / 3600.0;
                }
                else
                {
                    return null;
                }
            }
        }
        public Resolution resolution { get; set; }
        public int? aggregatetimeestimate { get; set; }
        public DateTime? resolutiondate { get; set; }
        public string summary { get; set; }
        public object[] subtasks { get; set; }
        public DateTime created { get; set; }
        public Priority priority { get; set; }
        public int? timeestimate { get; set; }
        public int? aggregatetimeoriginalestimate { get; set; }
        public string duedate { get; set; }
        public Progress progress { get; set; }
        public object[] issuelinks { get; set; }
        public Assignee assignee { get; set; }
        public DateTime updated { get; set; }
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

    public class Resolution
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
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


    public class Assignee
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string emailAddress { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
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
}
