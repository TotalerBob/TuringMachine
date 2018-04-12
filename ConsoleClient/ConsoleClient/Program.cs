using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            // ------------ TURING MACHINE SETUP ------------- //

            #region Turing machine
            // States
            List<string> states = new List<string>();
            states.Add("START");
            states.Add("COPY_B");
            states.Add("PASTE_B_0");
            states.Add("PASTE_B_1");
            states.Add("ADD_EQUAL");
            states.Add("BACK_B");
            states.Add("CLEAN_B_START");
            states.Add("CLEAN_B");
            states.Add("GOTO_A");
            states.Add("FILL_A");
            states.Add("GOTO_B");
            states.Add("READ_B");
            states.Add("FILL_B");
            states.Add("CLEAN_B");
            states.Add("GOTO_RESULT");
            states.Add("GOTO_VAR");
            states.Add("READ_VAR");
            states.Add("CLEAN_VAR");
            states.Add("WRITE_VAR_0");
            states.Add("WRITE_VAR_1");
            states.Add("WRITE_RESULT");
            states.Add("RESULT_TO_B");
            states.Add("BACK_TO_B");
            states.Add("CLEAN_CALCULATION");
            states.Add("GOTO_END");
            states.Add("COPY_RESULT");
            states.Add("COPY_RESULT_0");
            states.Add("COPY_RESULT_1");
            states.Add("GOTO_EQUAL");
            states.Add("CLEAN_END");
            states.Add("END");

            // Initial state
            string initialState = "START";

            // Final states
            List<string> finalStates = new List<string>();
            finalStates.Add("END");

            // Input symbols
            List<char> inputSymbols = new List<char>();
            inputSymbols.Add('0');
            inputSymbols.Add('1');
            inputSymbols.Add('*');

            // Symbols
            List<char> symbols = new List<char>();
            symbols.AddRange(inputSymbols);
            symbols.Add('X'); // 0
            symbols.Add('Y'); // 1
            symbols.Add('Z');
            symbols.Add('$');
            symbols.Add('=');

            // Empty symbol
            Char emptySymbol = '_';

            // Rules
            List<TuringRule> rules = new List<TuringRule>();
            #region rules
            rules.Add(new TuringRule("START", '0', "START", '0', 'R'));
            rules.Add(new TuringRule("START", '1', "START", '1', 'R'));
            rules.Add(new TuringRule("START", '*', "START", '*', 'R'));
            rules.Add(new TuringRule("START", '_', "COPY_B", '$', 'L'));

            rules.Add(new TuringRule("COPY_B", '0', "PASTE_B_0", 'X', 'R'));
            rules.Add(new TuringRule("COPY_B", '1', "PASTE_B_1", 'Y', 'R'));
            rules.Add(new TuringRule("COPY_B", 'X', "COPY_B", 'X', 'L'));
            rules.Add(new TuringRule("COPY_B", 'Y', "COPY_B", 'Y', 'L'));
            rules.Add(new TuringRule("COPY_B", '*', "ADD_EQUAL", '*', 'R'));

            rules.Add(new TuringRule("PASTE_B_0", '_', "BACK_B", '0', 'L'));
            rules.Add(new TuringRule("PASTE_B_0", '0', "PASTE_B_0", '0', 'R'));
            rules.Add(new TuringRule("PASTE_B_0", '1', "PASTE_B_0", '1', 'R'));
            rules.Add(new TuringRule("PASTE_B_0", '$', "PASTE_B_0", '$', 'R'));
            rules.Add(new TuringRule("PASTE_B_0", 'Y', "PASTE_B_0", 'Y', 'R'));
            rules.Add(new TuringRule("PASTE_B_0", 'X', "PASTE_B_0", 'X', 'R'));

            rules.Add(new TuringRule("PASTE_B_1", '_', "BACK_B", '1', 'L'));
            rules.Add(new TuringRule("PASTE_B_1", '0', "PASTE_B_1", '0', 'R'));
            rules.Add(new TuringRule("PASTE_B_1", '1', "PASTE_B_1", '1', 'R'));
            rules.Add(new TuringRule("PASTE_B_1", '$', "PASTE_B_1", '$', 'R'));
            rules.Add(new TuringRule("PASTE_B_1", 'Y', "PASTE_B_1", 'Y', 'R'));
            rules.Add(new TuringRule("PASTE_B_1", 'X', "PASTE_B_1", 'X', 'R'));

            rules.Add(new TuringRule("ADD_EQUAL", '0', "ADD_EQUAL", '0', 'R'));
            rules.Add(new TuringRule("ADD_EQUAL", '1', "ADD_EQUAL", '1', 'R'));
            rules.Add(new TuringRule("ADD_EQUAL", 'X', "ADD_EQUAL", 'X', 'R'));
            rules.Add(new TuringRule("ADD_EQUAL", 'Y', "ADD_EQUAL", 'Y', 'R'));
            rules.Add(new TuringRule("ADD_EQUAL", '$', "ADD_EQUAL", '$', 'R'));
            rules.Add(new TuringRule("ADD_EQUAL", '_', "BACK_TO_B", '=', 'L'));

            rules.Add(new TuringRule("BACK_TO_B", '0', "BACK_TO_B", '0', 'L'));
            rules.Add(new TuringRule("BACK_TO_B", '1', "BACK_TO_B", '1', 'L'));
            rules.Add(new TuringRule("BACK_TO_B", 'X', "BACK_TO_B", 'X', 'L'));
            rules.Add(new TuringRule("BACK_TO_B", 'Y', "BACK_TO_B", 'Y', 'L'));
            rules.Add(new TuringRule("BACK_TO_B", '$', "BACK_TO_B", '$', 'L'));
            rules.Add(new TuringRule("BACK_TO_B", '*', "CLEAN_B_START", '*', 'R'));

            rules.Add(new TuringRule("BACK_B", '0', "BACK_B", '0', 'L'));
            rules.Add(new TuringRule("BACK_B", '1', "BACK_B", '1', 'L'));
            rules.Add(new TuringRule("BACK_B", '$', "COPY_B", '$', 'L'));

            rules.Add(new TuringRule("CLEAN_B_START", 'X', "CLEAN_B_START", '0', 'R'));
            rules.Add(new TuringRule("CLEAN_B_START", 'Y', "CLEAN_B_START", '1', 'R'));
            rules.Add(new TuringRule("CLEAN_B_START", '$', "GOTO_A", '$', 'L'));

            rules.Add(new TuringRule("GOTO_A", '0', "GOTO_A", '0', 'L'));
            rules.Add(new TuringRule("GOTO_A", '1', "GOTO_A", '1', 'L'));
            rules.Add(new TuringRule("GOTO_A", '*', "READ_A", '*', 'L'));

            rules.Add(new TuringRule("READ_A", '0', "READ_A", '0', 'L'));
            rules.Add(new TuringRule("READ_A", '1', "FILL_A", '0', 'R'));
            rules.Add(new TuringRule("READ_A", '_', "CLEAN_CALCULATION", '_', 'R')); // END

            rules.Add(new TuringRule("FILL_A", '0', "FILL_A", '1', 'R'));
            rules.Add(new TuringRule("FILL_A", '*', "GOTO_B", '*', 'R'));

            rules.Add(new TuringRule("GOTO_B", '0', "GOTO_B", '0', 'R'));
            rules.Add(new TuringRule("GOTO_B", '1', "GOTO_B", '1', 'R'));
            rules.Add(new TuringRule("GOTO_B", '$', "READ_B", '$', 'L'));

            rules.Add(new TuringRule("FILL_B", '0', "FILL_B", '1', 'R'));
            rules.Add(new TuringRule("FILL_B", '$', "GOTO_RESULT", '$', 'R'));

            rules.Add(new TuringRule("READ_B", '0', "READ_B", '0', 'L'));
            rules.Add(new TuringRule("READ_B", '1', "FILL_B", '0', 'R'));
            rules.Add(new TuringRule("READ_B", '*', "CLEAN_B", '*', 'R'));

            rules.Add(new TuringRule("CLEAN_B", '0', "CLEAN_B", 'Z', 'R'));
            rules.Add(new TuringRule("CLEAN_B", '$', "READ_VAR", '$', 'R'));

            rules.Add(new TuringRule("GOTO_VAR", '0', "GOTO_VAR", '0', 'R'));
            rules.Add(new TuringRule("GOTO_VAR", '1', "GOTO_VAR", '1', 'R'));
            rules.Add(new TuringRule("GOTO_VAR", '$', "READ_VAR", '$', 'R'));

            rules.Add(new TuringRule("READ_VAR", '0', "WRITE_VAR_0", 'X', 'L'));
            rules.Add(new TuringRule("READ_VAR", '1', "WRITE_VAR_1", 'Y', 'L'));
            rules.Add(new TuringRule("READ_VAR", 'X', "READ_VAR", 'X', 'R'));
            rules.Add(new TuringRule("READ_VAR", 'Y', "READ_VAR", 'Y', 'R'));
            rules.Add(new TuringRule("READ_VAR", '=', "CLEAN_VAR", '=', 'L'));

            rules.Add(new TuringRule("WRITE_VAR_0", 'X', "WRITE_VAR_0", 'X', 'L'));
            rules.Add(new TuringRule("WRITE_VAR_0", 'Y', "WRITE_VAR_0", 'Y', 'L'));
            rules.Add(new TuringRule("WRITE_VAR_0", '$', "WRITE_VAR_0", '$', 'L'));
            rules.Add(new TuringRule("WRITE_VAR_0", '0', "WRITE_VAR_0", '0', 'L'));
            rules.Add(new TuringRule("WRITE_VAR_0", '1', "WRITE_VAR_0", '1', 'L'));
            rules.Add(new TuringRule("WRITE_VAR_0", 'Z', "GOTO_VAR", '0', 'R'));

            rules.Add(new TuringRule("WRITE_VAR_1", 'X', "WRITE_VAR_1", 'X', 'L'));
            rules.Add(new TuringRule("WRITE_VAR_1", 'Y', "WRITE_VAR_1", 'Y', 'L'));
            rules.Add(new TuringRule("WRITE_VAR_1", '$', "WRITE_VAR_1", '$', 'L'));
            rules.Add(new TuringRule("WRITE_VAR_1", '0', "WRITE_VAR_1", '0', 'L'));
            rules.Add(new TuringRule("WRITE_VAR_1", '1', "WRITE_VAR_1", '1', 'L'));
            rules.Add(new TuringRule("WRITE_VAR_1", 'Z', "GOTO_VAR", '1', 'R'));

            rules.Add(new TuringRule("CLEAN_VAR", 'X', "CLEAN_VAR", '0', 'L'));
            rules.Add(new TuringRule("CLEAN_VAR", 'Y', "CLEAN_VAR", '1', 'L'));
            rules.Add(new TuringRule("CLEAN_VAR", '$', "GOTO_A", '$', 'L'));

            rules.Add(new TuringRule("GOTO_RESULT", '0', "GOTO_RESULT", '0', 'R'));
            rules.Add(new TuringRule("GOTO_RESULT", '1', "GOTO_RESULT", '1', 'R'));
            rules.Add(new TuringRule("GOTO_RESULT", '=', "WRITE_RESULT", '=', 'R'));

            rules.Add(new TuringRule("WRITE_RESULT", '_', "RESULT_TO_B", '1', 'L'));
            rules.Add(new TuringRule("WRITE_RESULT", '0', "RESULT_TO_B", '1', 'L'));
            rules.Add(new TuringRule("WRITE_RESULT", '1', "WRITE_RESULT", '0', 'R'));

            rules.Add(new TuringRule("RESULT_TO_B", '=', "RESULT_TO_B", '=', 'L'));
            rules.Add(new TuringRule("RESULT_TO_B", '1', "RESULT_TO_B", '1', 'L'));
            rules.Add(new TuringRule("RESULT_TO_B", '0', "RESULT_TO_B", '0', 'L'));
            rules.Add(new TuringRule("RESULT_TO_B", '$', "READ_B", '$', 'L'));

            rules.Add(new TuringRule("CLEAN_CALCULATION", '0', "CLEAN_CALCULATION", '_', 'R'));
            rules.Add(new TuringRule("CLEAN_CALCULATION", '1', "CLEAN_CALCULATION", '_', 'R'));
            rules.Add(new TuringRule("CLEAN_CALCULATION", '*', "CLEAN_CALCULATION", '_', 'R'));
            rules.Add(new TuringRule("CLEAN_CALCULATION", '$', "CLEAN_CALCULATION", '_', 'R'));
            rules.Add(new TuringRule("CLEAN_CALCULATION", '=', "GOTO_END", '_', 'R'));

            rules.Add(new TuringRule("GOTO_END", '0', "GOTO_END", '0', 'R'));
            rules.Add(new TuringRule("GOTO_END", '1', "GOTO_END", '1', 'R'));
            rules.Add(new TuringRule("GOTO_END", '_', "COPY_RESULT", '=', 'L'));

            rules.Add(new TuringRule("COPY_RESULT", '0', "COPY_RESULT_0", 'Z', 'R'));
            rules.Add(new TuringRule("COPY_RESULT", '1', "COPY_RESULT_1", 'Z', 'R'));
            rules.Add(new TuringRule("COPY_RESULT", 'Z', "COPY_RESULT", 'Z', 'L'));
            rules.Add(new TuringRule("COPY_RESULT", '_', "CLEAN_END", '_', 'R'));

            rules.Add(new TuringRule("COPY_RESULT_0", 'Z', "COPY_RESULT_0", 'Z', 'R'));
            rules.Add(new TuringRule("COPY_RESULT_0", '1', "COPY_RESULT_0", '1', 'R'));
            rules.Add(new TuringRule("COPY_RESULT_0", '0', "COPY_RESULT_0", '0', 'R'));
            rules.Add(new TuringRule("COPY_RESULT_0", '=', "COPY_RESULT_0", '=', 'R'));
            rules.Add(new TuringRule("COPY_RESULT_0", '_', "GOTO_EQUAL", '0', 'L'));

            rules.Add(new TuringRule("COPY_RESULT_1", 'Z', "COPY_RESULT_1", 'Z', 'R'));
            rules.Add(new TuringRule("COPY_RESULT_1", '1', "COPY_RESULT_1", '1', 'R'));
            rules.Add(new TuringRule("COPY_RESULT_1", '0', "COPY_RESULT_1", '0', 'R'));
            rules.Add(new TuringRule("COPY_RESULT_1", '=', "COPY_RESULT_1", '=', 'R'));
            rules.Add(new TuringRule("COPY_RESULT_1", '_', "GOTO_EQUAL", '1', 'L'));

            rules.Add(new TuringRule("GOTO_EQUAL", '0', "GOTO_EQUAL", '0', 'L'));
            rules.Add(new TuringRule("GOTO_EQUAL", '1', "GOTO_EQUAL", '1', 'L'));
            rules.Add(new TuringRule("GOTO_EQUAL", '=', "COPY_RESULT", '=', 'L'));

            rules.Add(new TuringRule("CLEAN_END", 'Z', "CLEAN_END", '_', 'R'));
            rules.Add(new TuringRule("CLEAN_END", '=', "END", '_', 'R'));
            #endregion rules

            #endregion Turing machine


            // ----------------- USER INPUT ------------------ //

            #region User Input
            // Numbers to be calculated
            string number_a;
            string number_b;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("PLEASE ENTER FIRST NUMBER IN BINARY FORMAT: (e.g. 100101)");
                number_a = Console.ReadLine();
                Console.WriteLine("PLEASE ENTER SECOND NUMBER IN BINARY FORMAT: (e.g. 10111)");
                number_b = Console.ReadLine();
                Console.WriteLine("THE WORD TO BE PROCESSED WILL BE:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(number_a + "*" + number_b);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("IS THIS CORRECT? [Y/N]");
                char c = Console.ReadKey(true).KeyChar;
                if (c.Equals('Y') || c.Equals('y'))
                {
                    break;
                }
            }

            // Process mode
            TuringSettings s = new TuringSettings();
            Console.WriteLine("PLEASE ENTER THE PROCESS MODE: [AUTO, MANUAL, TIMED]");
            string processMode = Console.ReadLine();
            switch (processMode.ToLower())
            {
                case "auto":
                    s.StepMode = StepMode.AUTO;
                    break;
                case "manual":
                    s.StepMode = StepMode.MANUAL;
                    break;
                case "timed":
                    s.StepMode = StepMode.TIMED;
                    break;
                default:
                    s.StepMode = StepMode.MANUAL;
                    break;
            }

            // Step mode
            if (s.StepMode == StepMode.TIMED)
            {
                Console.WriteLine("HOW MANY MS TO WAIT BETWEEN EVERY CALCULATION STEP? [25 - 2000]");
                string ms_string = Console.ReadLine();
                int ms = Int16.Parse(ms_string);
                if (ms > 0)
                {
                    if (ms < 25)
                        ms = 25;
                    if (ms > 2000)
                        ms = 2000;
                }
                else
                {
                    ms = 500;
                }

                s.DoStepEveryXMilliseconds = ms;
            }

            // Output mode
            Console.WriteLine("PLEASE ENTER THE OUTPUT MODE: [COMMANDS, BAND, MIXED, NONE]");
            string outputMode = Console.ReadLine();
            switch (outputMode.ToLower())
            {
                case "commands":
                    s.OutputMode = OutputMode.COMMANDS;
                    break;
                case "band":
                    s.OutputMode = OutputMode.GRAPHICAL;
                    break;
                case "mixed":
                    s.OutputMode = OutputMode.MIXED;
                    break;
                case "none":
                    s.OutputMode = OutputMode.NONE;
                    break;
                default:
                    s.OutputMode = OutputMode.MIXED;
                    break;
            }
            #endregion User Input


            // Create turing machine
            TuringMachine tm = new TuringMachine(
                states: states,
                initialState: initialState,
                finalStates: finalStates,
                symbols: symbols,
                emptySymbol: emptySymbol,
                inputSymbols: inputSymbols,
                rules: rules
                );
            // Add user settings
            tm.SetSettings(s);

            // Execute TM
            try
            {
                tm.Start(number_a + "*" + number_b);
            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("INPUT WAS FAULTY, CHECK INPUT AND TRY AGAIN!");
            }

            // End
            Console.ReadKey(true);
        }
    }
}
