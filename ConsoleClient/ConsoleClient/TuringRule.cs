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



        public TuringRule(string currentState, char currentChar, string newState, char newChar, char direction)
        {
            this.CurrentState = currentState;
            this.CurrentChar = currentChar;
            this.NewState = newState;
            this.NewChar = newChar;
            this.Direction = direction;
        }
    }

    public class TuringRuleInput
    {
        public string CurrentState { get; set; }
        public char CurrentChar { get; set; }



        public override bool Equals(object obj)
        {
            return this.GetHashCode().Equals(obj.GetHashCode());
        }

        public override int GetHashCode()
        {
            return this.CurrentState.GetHashCode() * 17 + this.CurrentChar.GetHashCode();
        }
    }

    public class TuringRuleOutput
    {
        public string NewState { get; set; }
        public char NewChar { get; set; }
        public char Direction { get; set; }
    }
}
