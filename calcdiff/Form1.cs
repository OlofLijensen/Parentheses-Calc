using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calcdiff
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Inputfield_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string aa = InputField.Text;
            // Define a regular expression pattern to match numbers and blank spaces
            string pattern = @"[\s]";

            // Remove blank spaces using Regex.Replace()
            string bb = Regex.Replace(aa, pattern, "");

            string[] alist = bb.Split('+', '-', '*', '/');
            List<string> list = new List<string>();
            foreach (string al in alist)
            {
                list.Add(al);
                Console.WriteLine(al);
            }
            // Define a regular expression pattern to match numbers and blank spaces
            string patternb = @"[\d\s]";

            // Remove numbers and blank spaces using Regex.Replace()
            string result = Regex.Replace(aa, patternb, "");
            List<string> listb= new List<string>();
            foreach (char a in result)
            {
                listb.Add(a.ToString());
                Console.WriteLine(a);
            }
            List<string> merged = new List<string>();
            merged.Add(list[0]);
            for (int i = 1; i <= listb.Count;) 
            {
                merged.Add(listb[i-1]);
                merged.Add(list[i]);
                i++;
                
            }
            //list fixed//
            for (int i = 0; i < merged.Count;)
            {
                if (merged[i] == "*")
                {
                    Calculator calc = new Calculator(int.Parse(merged[i - 1]), int.Parse(merged[i+1]));
                    merged.RemoveAt(i + 1);
                    merged.RemoveAt(i);
                    merged.RemoveAt(i - 1);
                    merged.Insert(i - 1, calc.multiplication().ToString());
                    i = 0;

                    
                }
                else if (merged[i] == "/")
                {
                    Calculator calc = new Calculator(int.Parse(merged[i - 1]), int.Parse(merged[i + 1]));
                    merged.RemoveAt(i + 1);
                    merged.RemoveAt(i);
                    merged.RemoveAt(i - 1);
                    merged.Insert(i-1, calc.multiplication().ToString());
                    i = 0;
                }
                else { i++; }
            }
            for (int i = 0; i < merged.Count;)
            {
                if (merged[i] == "+")
                {
                    Console.WriteLine(i);
                    Console.WriteLine("hey");
                    Calculator calc = new Calculator(int.Parse(merged[i - 1]), int.Parse(merged[i + 1]));
                    merged.RemoveAt(i + 1);
                    merged.RemoveAt(i);
                    merged.RemoveAt(i - 1);
                    merged.Insert(i - 1, calc.addition().ToString());
                    i = 0;


                }
                else if (merged[i] == "-")
                {
                    Console.WriteLine("neh");
                    Calculator calc = new Calculator(int.Parse(merged[i - 1]), int.Parse(merged[i + 1]));
                    merged.RemoveAt(i + 1);
                    merged.RemoveAt(i);
                    merged.RemoveAt(i - 1);
                    merged.Insert(i - 1, calc.subtraktion().ToString());
                    i = 0;
                }
                else { i++; }
            }
            foreach (string i in merged)
            {
                Console.WriteLine(i);
            }
        }
    }
}
