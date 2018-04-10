using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class TuringSettings
    {
        public StepMode StepMode { get; set; } = StepMode.AUTO;
        public int DoStepEveryXMilliseconds { get; set; } = 100;
        public OutputMode OutputMode { get; set; } = OutputMode.GRAPHICAL;

    }

    public enum StepMode
    {
        MANUAL,
        AUTO,
        TIMED
    }

    public enum OutputMode
    {
        GRAPHICAL,
        COMMANDS,
        MIXED,
        NONE
    }
}
