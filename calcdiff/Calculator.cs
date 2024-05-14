using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calcdiff
{

    public class Calculator
    {
        //since this gets called by another class using internal is only possibility
        internal float num1;
        internal float num2;
         
        //constructor to not have to use parameters calculate class simpler to read and remeber
        internal Calculator(float num1, float num2) 
        {
            this.num1 = num1;
            this.num2 = num2;
        }

        internal float addition() { num1 += num2; return num1; }
        internal float subtraktion() { num1 -= num2; return num1; }
        internal float multiplication() { num1 *= num2; return num1; }
        internal float division() { num1 /= num2; return num1; }
    }}
