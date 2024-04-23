using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calcdiff
{
    internal class Newstringfix
    {
        internal List<string> newStringtoFix(string fixString)
        {
            //init vår string som vi vill cleara och fixa till en good beräkbar lista typ [ "8", "+", "8"]
            List<string> fixedString = new List<string>();
            //vi vill nu föröska lösa problemet hur vi vett att 888 inte är "8" "88" exempelvis
            int i = -1;
            string pattern = @"[\s]";
            string fixarrayCharFixedString = Regex.Replace(fixString, pattern, "");
            bool firstnum = true;
            string[] acceptableOperators = { "+", "-", "*", "/", "(", ")" };
            foreach (char value in fixarrayCharFixedString)
            {
                if (char.IsDigit(value))
                {
                    if (firstnum)
                    {
                        i++;
                        fixedString.Add(value.ToString());
                        firstnum = false;
                    }
                    else { fixedString[i] += value.ToString(); }

                }
                else if (value == '.' || value == ',')
                {
                    if (firstnum) { fixedString.Add("0" + value.ToString()); firstnum = false; }
                    else { fixedString[i] += value.ToString(); }
                }
                else if (Array.IndexOf(acceptableOperators, value.ToString()) == -1)
                {
                    fixedString.Clear();
                    fixedString.Insert(0, "noob"); 
                    break;

                }
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
