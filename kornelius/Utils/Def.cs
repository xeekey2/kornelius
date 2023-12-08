using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Utils
{
    internal class Def
    {
        public enum Resolution
        {
            Done = 10000,
            Fixed = 1,
            WontDo = 10100,
            WontFix = 2,
            Duplicate = 3,
            CannotReproduce = 5
        }
    }
}
