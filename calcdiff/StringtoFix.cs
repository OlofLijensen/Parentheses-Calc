using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace calcdiff
{
    internal class StringtoFix
    {
        private string stringtolist; 

        private StringtoFix(string stringtolist) 
        {
            this.stringtolist = stringtolist;
        }
        internal List<string> CalcReady(string stringtolist) 
        {

            // Define a regular expression pattern to match numbers and blank spaces
            string pattern = @"[\s]";

            // Remove blank spaces using Regex.Replace()
            string numebersString = Regex.Replace(stringtolist, pattern, "");

            string[] numbersList = numebersString.Split('+', '-', '*', '/');
            List<string> list = new List<string>();
            foreach (string numbers in numbersList)
            {
                list.Add(numbers);
            }
            // Define a regular expression pattern to match numbers and blank spaces
            string patternb = @"[\d\s]";

            // Remove numbers and blank spaces using Regex.Replace()
            string operattorstring = Regex.Replace(stringtolist, patternb, "");
            List<string> listb = new List<string>();
            foreach (char a in operattorstring)
            {
                listb.Add(a.ToString());
            }

            List<string> fixedlist = new List<string>();
            fixedlist.Add(list[0]);
            for (int i = 1; i <= listb.Count;)
            {
                fixedlist.Add(listb[i - 1]);
                fixedlist.Add(list[i]);
                i++;

            }

            return fixedlist;
        }
    }
}
