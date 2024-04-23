using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace calcdiff
{
    public class StringtoFix
    {
        internal string stringtolist; 
        internal List<string> CalcReady(string stringtolist) 
        {

            // Define a regular expression pattern to match numbers and blank spaces //
            string pattern = @"[\s]";

            // Remove blank spaces using Regex.Replace() abd pattern//
            string numebersString = Regex.Replace(stringtolist, pattern, "");

            // use opperators to split our nums// 
            string[] numbersArray = numebersString.Split('+', '-', '*', '/', '(', ')');


            // Define a regular expression pattern to match numbers and blank spaces //
            string patternb = @"[\d\s]";

            // Remove numbers and blank spaces using Regex.Replace() and pattern//
            string operatorArray = Regex.Replace(stringtolist, patternb, "");

            // if float is inserted remove . as its not opperator
            operatorArray = operatorArray.Replace(".", "");
            foreach (char op in operatorArray) { Console.WriteLine(op); }

            //since opperators and nums are fixed they are arrays and won change in size during calculations//
            //this list however does change as it reduces everytime its calced on therfore its list//
            List<string> fixedlist = new List<string>();

            //clean out empty values or null from our numbers list since 8(8) will give empty value on right of ")" since it is one of our spliters.//
            //safety precautions.
            numbersArray = numbersArray.Where(x => !string.IsNullOrEmpty(x)).ToArray();


            // cheack so we have more nums then opperators otherwise cant calc//
            if (numbersArray.Length == operatorArray.Length)
            {
                for ( int i = 0; i < numbersArray.Length; i++)
                {
                    fixedlist.Add(numbersArray[i].ToString());
                    fixedlist.Add(operatorArray[i].ToString());
                }
            }

            else if (numbersArray.Length >= operatorArray.Length)
            {
                //simply alternating one num from numArray and next one from opperatorArray add them alternatingly to our fixed list so the list gets fixed//
                fixedlist.Add(numbersArray[0].ToString());

                for (int i = 1; i < operatorArray.Length;)
                {
                    fixedlist.Add(operatorArray[i - 1].ToString());
                    fixedlist.Add(numbersArray[i].ToString());
                    i++;

                }
            }

            else { fixedlist[0] = "Retard can't calc"; }

            // return our now ready list to be calced list//
            return fixedlist;
        }
    }
}
