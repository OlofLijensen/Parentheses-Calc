using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calcdiff
{
    internal class Calculator
    {
        private int num1;
        private int num2;
         
        internal Calculator(int num1, int num2) 
        {
            this.num1 = num1;
            this.num2 = num2;
        }
        internal int addition() { num1 += num2; return num1; }
        internal int subtraktion() { num1 -= num2; return num1; }
        internal int multiplication() { num1 *= num2; return num1; }
        internal int division() { num1 /= num2; return num1; }
    }}
