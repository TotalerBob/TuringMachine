using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class TuringBand
    {
        private char[] turingBand;
        private int index;
        private char emptySymbol;



        public TuringBand(char emptySymbol)
        {
            this.emptySymbol = emptySymbol;
            Reset();
        }



        public void SetInitialWord(string word)
        {
            Reset();

            int tmpIndex = index;
            foreach (char c in word)
            {
                turingBand[tmpIndex] = c;
                tmpIndex++;
            }
        }

        public char Read()
        {
            return turingBand[index];
        }

        public void Write(char c, char direction)
        {
            turingBand[index] = c;
            if (direction.Equals('L'))
            {
                index--;
            }
            else
            {
                index++;
            }
        }

        public string GetGraphical(int lineLength)
        {
            StringBuilder b = new StringBuilder();
            int backAmount = (int)(lineLength / 2f) + 1;
            for (int j = index - backAmount; j < index + backAmount; j++)
            {
                b.Append(turingBand[j]);
            }

            return b.ToString();
        }

        public string GetWord()
        {
            StringBuilder b = new StringBuilder();
            int searchRange = 1000;
            for (int j = index - searchRange; j < index + searchRange; j++)
            {
                if (turingBand[j] != emptySymbol)
                {
                    b.Append(turingBand[j]);
                }
            }

            return b.ToString();
        }



        private void Reset()
        {
            turingBand = new char[2000000];
            index = (int)(1000000);
            for (int i = 0; i < turingBand.Length; i++)
            {
                turingBand[i] = '_';
            }
        }
    }
}
