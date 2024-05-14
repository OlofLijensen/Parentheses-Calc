using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calcdiff
{
    public class Stringfix
    {
        internal List<string> StringtoFix(string fixString)
        {
            //fix input string into list that is calcable ex [ "8", "+", "8"]
            List<string> fixedString = new List<string>();
            //biggest issue is to filter out space identify uncacable strings
            int i = -1;

            //clear space
            string pattern = @"[\s]";
            //use our pattern to clear space
            string fixarrayCharFixedString = Regex.Replace(fixString, pattern, "");

            //if it is continues of int aka 888 identifie it as 888 and not 8 8 8 
            bool firstnum = true;

            //identify what we can calc
            string[] acceptableOperators = { "+", "-", "*", "/", "(", ")" };

            //loop over string
            foreach (char value in fixarrayCharFixedString)
            {
                //if it is a digit aka 1 2 3 4 5 6 7 8 9 0 den go in here
                if (char.IsDigit(value))
                {
                    //if it is first of continues nums aka first 8 of 8123
                    if (firstnum)
                    {
                        // in our list increase index value 
                        i++;
                        //add it to our index value 
                        fixedString.Add(value.ToString());
                        //no longer our first num
                        firstnum = false;
                    }
                    //append it at current index value to for ex 8123 123goes here
                    else { fixedString[i] += value.ToString(); }

                }

                //fix input for floats
                else if (value == '.' || value == ',')
                {
                    //if it is our first num it will always be. so we fix it to 0, exemple so we can use it to calc
                    if (firstnum) { i++; fixedString.Add("0" + value.ToString()); firstnum = false; }
                }

                //if our string value is not digit and does not belong to . and acceptable opperators then
                else if (Array.IndexOf(acceptableOperators, value.ToString()) == -1)
                {
                    //just break and send error
                    fixedString.Clear();
                    fixedString.Insert(0, "error: Uncalc entry found!"); 
                    break;

                }
                // if it is inside accaptable opperators add it to list and fix to list and make our first num true since next will be first num
                else
                {
                    fixedString.Add(value.ToString());
                    i++;
                    firstnum = true;
                }
            }

            return fixedString;
        }
    }
}
