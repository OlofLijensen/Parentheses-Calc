using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calcdiff
{
    internal class Calculate
    {
        private List<string> list; 
        internal string CalculatIt(List<string> list) 
        {
            List<int> leftParantesesIndex = new List<int>();
            List<int> rightParentesesIndex = new List<int>();
            int leftParantesesIndexValue = 0;
            int rightParantesesIndexValue = 0;
            foreach (var item in list) { Console.WriteLine(item); }
            for (int i = 0; i < list.Count; i++)
            {
               if (list[i] == "(")
               {
                    Console.WriteLine("here1");
                    leftParantesesIndex.Insert(leftParantesesIndexValue,i);
                    leftParantesesIndexValue++;
               }
               else if(list[i] == ")") 
               {
                    Console.WriteLine("here2");
                    rightParentesesIndex.Insert(rightParantesesIndexValue, i);
                    rightParantesesIndexValue++;
               }
                else { continue; }
            }

            leftParantesesIndex.Reverse();
            int countLeftParanteseIndex = 0;
            int countRightParanteseIndex = 0;

            foreach(int a in  leftParantesesIndex) { Console.WriteLine(a); }
            foreach(int b in rightParentesesIndex) { Console.WriteLine(b); }

            //for loop to methodicaly calculate whole string we start with * / since they have prio//
            for (int i = leftParantesesIndex[countLeftParanteseIndex]; i == rightParentesesIndex[countRightParanteseIndex];)
            {
                if(i == rightParentesesIndex[countRightParanteseIndex])
                {
                    countRightParanteseIndex++;
                    countLeftParanteseIndex++;
                    continue;
                }

                //calc proccess is from left to right therfore no worries to start from list index 0//
                else if (list[i] == "*")
                {
                    //when we find first encounterd * from left and since input is fixed i.e["8", "*", "8"] for ex we only need to find operators left and right num and calc with corresponding opperator for right result.//
                    //left and right num get calculated with calcutlator class//
                    //convert string to float so we need CultureInfo.InvariantCulture to fix so all use . instead of , when desplaying floats.//
                    Calculator calc = new Calculator(float.Parse(list[i - 1], CultureInfo.InvariantCulture), float.Parse(list[i + 1], CultureInfo.InvariantCulture));

                    //remove from list old already calced nums and opperators//
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i);
                    list.RemoveAt(i - 1);

                    //insert at left nums pose to make our list right also calc our nums with right opperator and replace if float , with . to make it reusable.//
                    list.Insert(i - 1, calc.multiplication().ToString().Replace(",", "."));

                    //list order changed so restart search of list from left//
                    i = 0;
                }

                //same as * just for searching for / //
                else if (list[i] == "/")
                {
                    Calculator calc = new Calculator(float.Parse(list[i - 1], CultureInfo.InvariantCulture), float.Parse(list[i + 1], CultureInfo.InvariantCulture));
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i);
                    list.RemoveAt(i - 1);
                    list.Insert(i - 1, calc.division().ToString().Replace(",", "."));
                    i = 0;
                }

                else { i++; }
            }

            // since * / has prio they are calced first after + - but process is same so no need for comments//
            for (int i = 0; i < list.Count;)
            {
                if (list[i] == "+")
                {

                    Calculator calc = new Calculator(float.Parse(list[i - 1], CultureInfo.InvariantCulture), float.Parse(list[i + 1], CultureInfo.InvariantCulture));
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i);
                    list.RemoveAt(i - 1);
                    list.Insert(i - 1, calc.addition().ToString().Replace(",", "."));
                    i = 0;


                }

                else if (list[i] == "-")
                {
                    Calculator calc = new Calculator(float.Parse(list[i - 1], CultureInfo.InvariantCulture), float.Parse(list[i + 1], CultureInfo.InvariantCulture));
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i);
                    list.RemoveAt(i - 1);
                    list.Insert(i - 1, calc.subtraktion().ToString().Replace(",", "."));
                    i = 0;
                }
                else { i++; }
            }
            string output = string.Join("",list);
            return output;
        }
    }
}
