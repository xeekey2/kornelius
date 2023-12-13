using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.MVVM.Model
{
    public class Settings
    {
        public string JiraUsername { get; set; }
        public string JiraBaseUrl { get; set; }
        public bool EnableAutomaticLogging { get; set; }
        public bool ShowEstimatedTime { get; set; }
        public bool ShowLoggedTime { get; set; }
        public bool ShowRemainingTime { get; set; }
    }
}
