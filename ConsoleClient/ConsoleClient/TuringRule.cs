using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class TuringRule
    {
        public string CurrentState { get; set; }
        public char CurrentChar { get; set; }

        public string NewState { get; set; }
        public char NewChar { get; set; }
        public char Direction { get; set; }
    }
}
