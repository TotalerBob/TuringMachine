using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class TuringMachine
    {
        private readonly string[] _states;
        private readonly char[] _symbols;
        private readonly char _emptySymbol;
        private readonly char[] _inputSymbols;
        private readonly string _initialState;
        private readonly string[] _finalStates;
        private readonly TuringRule[] _rules;

        private TuringBand turingBand;
        private TuringSettings settings;
        private string currentState;



        public TuringMachine(
            List<string> states,
            string initialState,
            List<string> finalStates,
            List<char> symbols,
            char emptySymbol,
            List<char> inputSymbols,
            List<TuringRule> rules
            )
        {
            this._states = states.ToArray();
            this._symbols = symbols.ToArray();
            this._emptySymbol = emptySymbol;
            this._inputSymbols = inputSymbols.ToArray();
            this._initialState = initialState;
            this._finalStates = finalStates.ToArray();
            this._rules = rules.ToArray();

            this.turingBand = new TuringBand();
            this.settings = new TuringSettings();
        }



        public void Start(string word)
        {
            // Reset machine
            currentState = _initialState;

            // Write word to band
            turingBand.SetInitialWord(word);


        }
    }
}
