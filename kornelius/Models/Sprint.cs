using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Models
{
    public class Sprint
    {
        public bool Closed { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int RapidViewId { get; set; }
        public bool Started { get; set; }
        public DateTime? StartDate { get; set; }
        public int Sequence { get; set; }
    }
}
