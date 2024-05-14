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
            //get input string and store it in a var to be able to calc//
            string inputstring = InputField.Text;

            //init the string fixing class
            Stringfix stringfix = new Stringfix();

            //fix string with our class
            List<string> merged = stringfix.StringtoFix(inputstring);

            //init the calculater//
            Calculate calculate = new Calculate();

            //head over to class called calculate for additanol comments on procces and how it works//
            calculate.CalculatIt(merged);

            //output our calculated value of our input string and output in ouput field//
            Outputfield.Text = string.Join("", merged);
        }

        private void Outputfield_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
