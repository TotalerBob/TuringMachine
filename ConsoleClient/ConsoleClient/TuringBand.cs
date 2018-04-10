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



        public TuringBand()
        {
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

        private void Reset()
        {
            turingBand = new char[Int32.MaxValue];
            index = (int)(Int32.MaxValue / 2);
        }
    }
}
