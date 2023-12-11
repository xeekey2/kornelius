using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Models
{

    public class BoardResponse
    {
        public int maxResults { get; set; }
        public int startAt { get; set; }
        public bool isLast { get; set; }
        public Board[] values { get; set; }
    }

    public class Board
    {
        public int id { get; set; }
        public string self { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public override string ToString()
        {
            return name;
        }
    }

}
