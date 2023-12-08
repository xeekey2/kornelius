using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Models
{
    public class Issue
    {
        public long Project { get; set; }

        public string Assignee { get; set; }

        public long IssueType { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string Resolution { get; set; }

        public string IssueStatus { get; set; }

        public long? TimeOriginalEstimate { get; set; }

        public long? TimeEstimate { get; set; }

        public long? TimeSpent { get; set; }

        public override string ToString()
        {
            return Summary;
        }
    }
}
