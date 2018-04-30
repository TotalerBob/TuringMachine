using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ConsoleClient
{
    public class TuringMachine
    {
        // Console window settings
        private static int WINDOW_WIDTH = 101;
        private static int WINDOW_HEIGHT = 30;
        private static int TITLE_LN = 0;
        private static int TITLE_BOTTOMLINE_LN = 1;

        // TM Vars
        private readonly HashSet<string> _states;
        private readonly HashSet<char> _symbols;
        private readonly char _emptySymbol;
        private readonly HashSet<char> _inputSymbols;
        private readonly string _initialState;
        private readonly HashSet<string> _finalStates;
        private readonly Dictionary<TuringRuleInput, TuringRuleOutput> _rules = new Dictionary<TuringRuleInput, TuringRuleOutput>();

        private TuringBand turingBand;
        private TuringSettings settings;
        private string currentState;
        private Stopwatch executionTime;
        private int executionSteps = 0;

        // Output commands
        private List<string> commands { get; set; } = new List<string>();
        
        // Display vars
        private delegate void Display();
        private Display display;
        private Timer t;



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
            // Output preparations
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            // Values
            this._states = new HashSet<string>(states);
            this._symbols = new HashSet<char>(symbols);
            this._emptySymbol = emptySymbol;
            this._inputSymbols = new HashSet<char>(inputSymbols);
            this._initialState = initialState;
            this._finalStates = new HashSet<string>(finalStates);
            foreach (TuringRule r in rules)
            {
                TuringRuleInput i = new TuringRuleInput()
                {
                    CurrentChar = r.CurrentChar,
                    CurrentState = r.CurrentState
                };
                TuringRuleOutput o = new TuringRuleOutput()
                {
                    Direction = r.Direction,
                    NewChar = r.NewChar,
                    NewState = r.NewState
                };

                this._rules.Add(i, o);
                
            }         

            this.turingBand = new TuringBand(_emptySymbol);
            this.settings = new TuringSettings();
        }



        /// <summary>
        /// Starts the TM
        /// </summary>
        /// <param name="word">The word to be processed</param>
        public void Start(string word)
        {
            // Reset machine
            currentState = _initialState;
            Console.Clear();
            Console.SetCursorPosition(0, TITLE_LN);
            Console.Write("Processing word: " + word);
            Console.SetCursorPosition(0, TITLE_BOTTOMLINE_LN);
            Console.Write(new String('-', WINDOW_WIDTH));

            // Write word to band
            turingBand.SetInitialWord(word);

            // Read settings
            switch (settings.OutputMode)
            {
                case OutputMode.COMMANDS:
                    display = DisplayCommands;
                    display();
                    break;
                case OutputMode.GRAPHICAL:
                    display = DisplayGraphical;
                    display();
                    break;
                case OutputMode.MIXED:
                    display = DisplayMixed;
                    display();
                    break;
                case OutputMode.NONE:
                    display = DisplayNothing;
                    break;
            }

            // Start execution time
            executionTime = new Stopwatch();
            executionTime.Start();

            // Start in certain mode defined by settings
            bool running = true;
            switch (settings.StepMode)
            {
                case StepMode.AUTO:
                    while (running)
                    {
                        running = DoStep();
                    }

                    break;
                case StepMode.MANUAL:
                    while (running)
                    {
                        Console.ReadKey(true);
                        running = DoStep();                     
                    }
                    break;
                case StepMode.TIMED:
                    while (running)
                    {
                        Thread.Sleep(settings.DoStepEveryXMilliseconds);
                        running = DoStep();
                    }
                    break;
            }
        }

        /// <summary>
        /// Setter for settings
        /// </summary>
        /// <param name="s">Settings of the turingmachine</param>
        public void SetSettings(TuringSettings s)
        {
            settings = s;
        }

        private bool DoStep()
        {
            // Read band
            char c = turingBand.Read();

            // Check if word is in a state to be accepted
            if (_finalStates.Contains(currentState))
            {
                DisplayEnd();
                return false;
            }

            // Execute rule
            TuringRuleOutput o = _rules[new TuringRuleInput() {CurrentChar = c, CurrentState = currentState}];
            currentState = o.NewState;
            turingBand.Write(o.NewChar, o.Direction);
            

            // Add command
            commands.Add($"State: {o.NewState}, Write: {o.NewChar}, Dir: {o.Direction}");
            executionSteps++;

            // Display
            display();
            return true;
        }

        private void DisplayEnd()
        {
            executionTime.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, WINDOW_HEIGHT - 4);
            Console.Write(new String('-', WINDOW_WIDTH));
            Console.SetCursorPosition(0, WINDOW_HEIGHT - 1);
            Console.Write(new String('-', WINDOW_WIDTH));

            float seconds = executionTime.ElapsedMilliseconds / 1000f;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, WINDOW_HEIGHT - 3);
            Console.Write($"Operation completed in " + ((seconds > 1) ? seconds + "s" : executionTime.ElapsedMilliseconds + "ms") + " in " + executionSteps + " calculation steps. The final state was '" + this.currentState + "'.");
            Console.SetCursorPosition(0, WINDOW_HEIGHT - 2);

            string word = turingBand.GetWord();
            if (word.Length < WINDOW_WIDTH)
            {
                word += new String(' ', WINDOW_WIDTH - word.Length - 1);
            }
            Console.Write("The output word is: ");
            Console.ForegroundColor = ConsoleColor.Green;            
            Console.Write(word);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void DisplayCommands()
        {
            if (commands.Count > WINDOW_HEIGHT - 2)
            {
                commands.RemoveAt(0);
            }

            int ln = TITLE_BOTTOMLINE_LN + 1;
            foreach (string cmd in commands)
            {
                Console.SetCursorPosition(0, ln);
                int whitespaces = WINDOW_WIDTH - cmd.Length - 1;
                Console.Write(cmd + new String(' ', whitespaces));
                ln++;
            }      
        }

        private void DisplayGraphical()
        {
            Console.SetCursorPosition((int)(Math.Ceiling(WINDOW_WIDTH / 2d)), TITLE_BOTTOMLINE_LN + 1);
            Console.Write("V");
            Console.SetCursorPosition(0, TITLE_BOTTOMLINE_LN + 2);
            Console.Write(turingBand.GetGraphical(WINDOW_WIDTH));
            Console.SetCursorPosition(0, TITLE_BOTTOMLINE_LN + 3);
            Console.Write(new String('-', WINDOW_WIDTH));
        }

        private void DisplayMixed()
        {
            Console.SetCursorPosition((int)(Math.Ceiling(WINDOW_WIDTH / 2d)), TITLE_BOTTOMLINE_LN + 1);
            Console.Write("V");
            Console.SetCursorPosition(0, TITLE_BOTTOMLINE_LN + 2);
            Console.Write(turingBand.GetGraphical(WINDOW_WIDTH));
            Console.SetCursorPosition(0, TITLE_BOTTOMLINE_LN + 3);
            Console.Write(new String('-', WINDOW_WIDTH));

            if (commands.Count > WINDOW_HEIGHT - 5)
            {
                commands.RemoveAt(0);
            }

            int ln = TITLE_BOTTOMLINE_LN + 4;
            foreach (string cmd in commands.ToArray())
            {
                Console.SetCursorPosition(0, ln);
                int whitespaces = WINDOW_WIDTH - cmd.Length - 1;
                Console.Write(cmd + new String(' ', whitespaces));
                ln++;
            }
        }

        private void DisplayNothing()
        {
            commands.RemoveAt(0);
        }
    }
}