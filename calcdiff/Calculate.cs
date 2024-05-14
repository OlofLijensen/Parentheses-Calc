using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calcdiff
{
    public class Calculate
    {
        internal List<string> list; 
        internal string CalculatIt(List<string> list) 
        {
            //We use paranthesis as our start point since they need to be dealt with first. 
            //Therefore we creat a list of index poses for parantheis so we know start and endpoint of each loop
            //This is what these list var are used for.
            List<int> leftParantesesIndex = new List<int>();
            List<int> rightParentesesIndex = new List<int>();


            //how many parenthis have processed
            int timestocalced = 0;

            //insert att begining of input list and end to be able to handle lists without parenthesis and also compelete calcs
            list.Insert(0, "(");
            list.Insert(list.Count, ")");


            //how pass important our parantheis is ex: (8(1+1) + (1+1))
            //here our order of priority in parantheis is both 2 on the secound and the third parantheis so their prio is the same
            int leftprio = 0;

            //list of left and right parantheis postion in orignial input list and also their prio
            List<int[]> listOfArrays = new List<int[]>();
            List<int[]> listOfArrays2 = new List<int[]>();

            //find left and right parantheis their index value in orignal string and their prio the left and right parantheis has the same lvl prio (prio increase by 1 per left parantheis past and minus 1 per right parantheis passed)
            for ( int i = 0; i< list.Count; i++)
            {
                if (list[i] == "(")
                {
                    leftprio++;
                    listOfArrays.Add(new int[] {i,leftprio });
                    

                }
                else if (list[i] == ")")
                {
                    listOfArrays2.Add(new int[] {i,leftprio});
                    //if right parantheis we want the deacrease to happen after appendning it since we want to pair right and left
                    leftprio--;

                }
                else { continue; }
            }

            //Coppy of list of left and right parantheis list arrays these will be swapped aroound trying to find our order of calc
            List<int[]> copylistOfArrays = listOfArrays.Select(array => array.ToArray()).ToList();
            List<int[]> copylistOfArrays2 = listOfArrays2.Select(array => array.ToArray()).ToList();

            //loop iterations of the number of items in our list
            //strat for how to order our list is first by prio in both list, if prio is same then we go by lowest index value for both left and right parantheis
            for (int i = 0; i < listOfArrays.Count; i++)
            {
                //bigger then increase by one for every array that is bigger then all other arrays, 
                //indexpos is its index postion which will get uppdated in the copy list
                int biggerthen = 0; 
                int indexpos = listOfArrays.Count - 1;

                //for every array we need to loop over every other array to check if its bigger
                for (int j = 0; j < listOfArrays.Count; j++)
                {

                    //if it is itself
                    if (listOfArrays[i][1] == listOfArrays[j][1] && listOfArrays[i][0] == listOfArrays[j][0]) 
                    {
                        //if its the last iteration
                        if (j == listOfArrays.Count - 1)
                        {
                            //find index positon
                            indexpos -= biggerthen;
                            //remove and replace old value to update
                            copylistOfArrays.RemoveAt(indexpos);
                            copylistOfArrays.Insert(indexpos, listOfArrays[i]);
                        }
                        //otherwise just keep going
                        else { continue; }
                    }

                    //if it has same prio lvl with another paranthesis we want to find lowest
                    else if(listOfArrays[i][1] == listOfArrays[j][1])
                    {
                        //if it is lower
                        if (listOfArrays[i][0] < listOfArrays[j][0])
                        {
                            //then it is "bigger"
                            biggerthen++;

                            //if it is the last iteration do the necessary replacment
                            if (j == listOfArrays.Count - 1)
                            {
                                indexpos -= biggerthen;
                                copylistOfArrays.RemoveAt(indexpos);
                                copylistOfArrays.Insert(indexpos, listOfArrays[i]);
                            }
                            //keep going if not
                            else { continue; }
                        }
                        //keep going if not
                        else { continue; }
                    }
                    //if it has bigger prio
                    else if (listOfArrays[i][1] > listOfArrays[j][1])
                    {
                        //its then bigger
                        biggerthen++;
                        //if last do the neccesary replacments 
                        if (j == listOfArrays.Count - 1)
                        {
                            indexpos -= biggerthen;
                            copylistOfArrays.RemoveAt(indexpos);
                            copylistOfArrays.Insert(indexpos, listOfArrays[i]);
                        }
                    }
                    //if its not either of these cases its smaller
                    else
                    {
                        //if last do the necessary
                        if (j == listOfArrays.Count - 1)
                        {
                            indexpos -= biggerthen;
                            copylistOfArrays.RemoveAt(indexpos);
                            copylistOfArrays.Insert(indexpos, listOfArrays[i]);
                        }
                        //continue otherwise
                        else { continue; }
                    }
                }
            }

            //exactly the same process as the one above but for the other list
            for (int i = 0; i < listOfArrays2.Count; i++)
            {
                int biggerthen = 0;
                int indexpos = listOfArrays2.Count - 1;
                for (int j = 0; j < listOfArrays2.Count; j++)
                {

                    if (listOfArrays2[i][1] == listOfArrays2[j][1] && listOfArrays2[i][0] == listOfArrays2[j][0])
                    {
                        if (j == listOfArrays2.Count - 1)
                        {
                            indexpos -= biggerthen;
                            copylistOfArrays2.RemoveAt(indexpos);
                            copylistOfArrays2.Insert(indexpos, listOfArrays2[i]);
                        }
                        else { continue; }
                    }

                    else if (listOfArrays2[i][1] == listOfArrays2[j][1])
                    {
                        if (listOfArrays2[i][0] < listOfArrays2[j][0])
                        {
                            biggerthen++;
                            if (j == listOfArrays2.Count - 1)
                            {
                                indexpos -= biggerthen;
                                copylistOfArrays2.RemoveAt(indexpos);
                                copylistOfArrays2.Insert(indexpos, listOfArrays2[i]);
                            }
                            else { continue; }
                        }
                        else 
                        {
                            if (j == listOfArrays2.Count - 1)
                            {
                                indexpos -= biggerthen;
                                copylistOfArrays2.RemoveAt(indexpos);
                                copylistOfArrays2.Insert(indexpos, listOfArrays2[i]);
                            }
                            else { continue; }
                        }
                    }
                    else if (listOfArrays2[i][1] > listOfArrays2[j][1])
                    {
                        biggerthen++;
                        if (j == listOfArrays2.Count - 1)
                        {
                            indexpos -= biggerthen;
                            copylistOfArrays2.RemoveAt(indexpos);
                            copylistOfArrays2.Insert(indexpos, listOfArrays2[i]);
                        }
                        else { continue; }
                    }
                    else
                    {
                        if (j == listOfArrays2.Count - 1)
                        {
                            indexpos -= biggerthen;
                            copylistOfArrays2.RemoveAt(indexpos);
                            copylistOfArrays2.Insert(indexpos, listOfArrays2[i]);
                        }
                        else { continue; }
                    }
                }
            }
            
            //clean the list remove prio from our array list and only have the indexes
            foreach (int[] a in copylistOfArrays)
            {
                leftParantesesIndex.Add(a[0]);
            }
            foreach (int[] a in copylistOfArrays2)
            {
                rightParentesesIndex.Add(a[0]);
            }


            //index value of left and right 
            int countLeftParanteseIndex = 0;
            int countRightParanteseIndex = 0;
            
            //now the reader of this code should embrace onself for alot of logic gates, the purpose of these are to handle execeptios
            bool firstpassthrough = true;

            //we loop over our parnthesis 8(8+8) we want to have our startpoint at the left paranthesis plus 1 and have our endpoint at right parntehsis. after this we want run through the code where our index 0, 8 and we treat ( as a *
            for (int i = leftParantesesIndex[countLeftParanteseIndex]+1; i <= rightParentesesIndex[countRightParanteseIndex];) 
            {
                //when treating our parantehsis based calc we want to go through a calculations where we start calculating * / and if their is (
                if (firstpassthrough)
                {
                    //if we are at our last point of calculations for our first passthrough / * and pot (
     
                    if (i == rightParentesesIndex[countRightParanteseIndex])
                    {
                        //reset startpoint change firstpassthrough so we no longer calc / * (
                        i = leftParantesesIndex[countLeftParanteseIndex] + 1;
                        firstpassthrough = false;
                    }

                    //if we at this point of our list is att this opperator
                    else if (list[i] == "*")
                    {
                        //handle execpetions since ( and ) arent nums therefore if left of * is ( or ) we want to remove it since it serves no purpose
                        if (list[i + 1] == "(" || list[i - 1] == ")")
                        {
                            //if found we want to remove it
                            list.RemoveAt(i + 1);
                            //now we have to uppdate our lists of start points and enpoints aka our list of index values for both ( and )
                            for (int j = 0; rightParentesesIndex.Count() > j; j++)
                            {
                                //if its index value is higer then the index value of our removed ( or ) we want to uppdate its value since its pos now switch with -1
                                if (i + 1 < rightParentesesIndex[j])
                                {
                                    rightParentesesIndex[j] -= 1;
                                }
                            }
                            //same as above but for the other list )
                            for (int j = 0; leftParantesesIndex.Count() > j; j++)
                            {
                                if (i + 1 < leftParantesesIndex[j])
                                {
                                    leftParantesesIndex[j] -= 1;
                                }
                            }
                        }
                        else { }

                        //same thing but if its to the left ex ) + ( left side needs to be removed
                        //same concept with uppdating things, aka list index positions in list
                        if (list[i - 1] == ")" || list[i - 1] == "(")
                        {
                            list.RemoveAt(i - 1);
                            for (int j = 0; rightParentesesIndex.Count() > j; j++)
                            {

                                if (i - 1 < rightParentesesIndex[j])
                                {
                                    rightParentesesIndex[j] -= 1;
                                }
                            }
                            for (int j = 0; leftParantesesIndex.Count() > j; j++)
                            {
                                if (i - 1 < leftParantesesIndex[j])
                                {
                                    leftParantesesIndex[j] -= 1;
                                }
                            }
                            //if we remove something behinde our current pose we want to shift back effectively 
                            i--;
                        }

                        //handel poor user input for ex 8**8
                        try
                        {
                            //Constructor where we pase our values but converted from string to int where we use CultureInfo.InvariantCulture to consitently get the right decimal
                            Calculator calc = new Calculator(float.Parse(list[i - 1], CultureInfo.InvariantCulture), float.Parse(list[i + 1], CultureInfo.InvariantCulture));
                            //remove from list old already calced nums and opperators since it calcs using 3 values where opperator is orignal we remove front and back
                            list.RemoveAt(i + 1);
                            list.RemoveAt(i);
                            list.RemoveAt(i - 1);

                            //insert at left nums pose to make our list right also calc our nums with right opperator and replace if float , with . to make it reusable.//
                            list.Insert(i - 1, calc.multiplication().ToString().Replace(",", "."));
                        }

                        //error to catch and also add 2 extra items to list since we delete 2 items at the end of the code
                        catch(FormatException) { list.Clear(); list.AddRange(new string[] { "noob", "error: Formating issue!", "noob" }); ; break; }

                        //same as the uppdate above with the ( and ) but now minus 2 since we remove 3 add 1
                        for (int j = 0; rightParentesesIndex.Count() > j; j++)
                        {
                            if (i < rightParentesesIndex[j])
                            {
                                rightParentesesIndex[j] -= 2;
                            }
                        }
                        for (int j = 0; leftParantesesIndex.Count() > j; j++)
                        {
                            if (i < leftParantesesIndex[j])
                            {
                                leftParantesesIndex[j] -= 2;
                            }
                        }
                        //reset our postional poinst to make sure 8*8*8 to the begining of our start point to make sure it all gets calced correct
                        //although there are beter ways these calcs aka more effective less calcing but this just makes sure since its anyways neglibale 
                        i = leftParantesesIndex[countRightParanteseIndex]+1;
                    }

                    //exactly the same purpose as the one above.
                    else if (list[i] == "/") 
                    {
                        if (list[i + 1] == "(" || list[i - 1] == ")")
                        {
                            list.RemoveAt(i + 1);
                            for (int j = 0; rightParentesesIndex.Count() > j; j++)
                            {
                                if (i + 1 < rightParentesesIndex[j])
                                {
                                    rightParentesesIndex[j] -= 1;
                                }
                            }
                            for (int j = 0; leftParantesesIndex.Count() > j; j++)
                            {
                                if (i + 1 < leftParantesesIndex[j])
                                {
                                    leftParantesesIndex[j] -= 1;
                                }
                            }
                        }
                        else { }

                        if (list[i - 1] == ")" || list[i - 1] == "(")
                        {
                            list.RemoveAt(i - 1);
                            for (int j = 0; rightParentesesIndex.Count() > j; j++)
                            {
                                if (i - 1 < rightParentesesIndex[j])
                                {
                                    rightParentesesIndex[j] -= 1;
                                }
                            }
                            for (int j = 0; leftParantesesIndex.Count() > j; j++)
                            {
                                if (i - 1 < leftParantesesIndex[j])
                                {
                                    leftParantesesIndex[j] -= 1;
                                }
                            }
                            i--;
                        }

                        //handel poor user input for ex 8**8
                        try
                        {
                            //Constructor where we pase our values but converted from string to int where we use CultureInfo.InvariantCulture to consitently get the right decimal
                            Calculator calc = new Calculator(float.Parse(list[i - 1], CultureInfo.InvariantCulture), float.Parse(list[i + 1], CultureInfo.InvariantCulture));
                            //remove from list old already calced nums and opperators since it calcs using 3 values where opperator is orignal we remove front and back
                            list.RemoveAt(i + 1);
                            list.RemoveAt(i);
                            list.RemoveAt(i - 1);

                            //insert at left nums pose to make our list right also calc our nums with right opperator and replace if float , with . to make it reusable.//
                            list.Insert(i - 1, calc.division().ToString().Replace(",", "."));
                        }

                        //error to catch and also add 2 extra items to list since we delete 2 items at the end of the code
                        catch (FormatException) { list.Clear(); list.AddRange(new string[] { "noob", "error: Formating issue!", "noob" }); ; break; }

                        for (int j = 0; rightParentesesIndex.Count() > j; j++)
                        {
                            if (i < rightParentesesIndex[j])
                            {
                                rightParentesesIndex[j] -= 2;
                            }
                        }
                        for (int j = 0; leftParantesesIndex.Count() > j; j++)
                        {
                            if (i < leftParantesesIndex[j])
                            {
                                leftParantesesIndex[j] -= 2;
                            }
                        }
                        i = leftParantesesIndex[countRightParanteseIndex] + 1;
                    }

                    //now same with ( and ) where it will be treated as *
                    else if (list[i] == "(" || list[i] == ")")
                    {
                        //same thing with uppdating to ( ) to keep it all consistan and clean see line (248 and down)
                        if (list[i + 1] == "(" || list[i + 1] == ")")
                        {
                            list.RemoveAt(i + 1);

                            for (int j = 0; rightParentesesIndex.Count() > j; j++)
                            {
                                if (i < rightParentesesIndex[j])
                                {
                                    rightParentesesIndex[j] -= 1;
                                }
                            }
                            for (int j = 0; leftParantesesIndex.Count() > j; j++)
                            {
                                if (i < leftParantesesIndex[j])
                                {
                                    leftParantesesIndex[j] -= 1;
                                }
                            }
                            continue;
                        }
                        //same here find see lind and comments 248 and down
                        if (list[i - 1] == ")" || list[i + 1] == "(")
                        {
                            list.RemoveAt(i - 1);
                            for (int j = 0; rightParentesesIndex.Count() > j; j++)
                            {
                                if (i - 1 < rightParentesesIndex[j])
                                {
                                    rightParentesesIndex[j] -= 1;
                                }
                            }
                            for (int j = 0; leftParantesesIndex.Count() > j; j++)
                            {
                                if (i -  1 < leftParantesesIndex[j])
                                {
                                    leftParantesesIndex[j] -= 1;
                                }
                            }
                        }
                        //if a "usefull" opperators is is in front or behind it then we just pass this possition since i dont want to handle this inside here but rather at the "usefull" opperators
                        if (list[i-1]=="+" || list[i-1]=="-"|| list[i - 1] == "*" || list[i - 1] == "/")
                        {
                            i++;
                            continue;
                        }
                        else if (list[i + 1] == "+" || list[i + 1] == "-" || list[i + 1] == "*" || list[i + 1] == "/")
                        {
                            i++;
                            continue;
                        }

                        // if it dosent have a "usefull" opperators closeby we can trat ( or ) as a * se line 295
                        else
                        {
                            //handel poor user input for ex 8**8
                            try
                            {
                                //Constructor where we pase our values but converted from string to int where we use CultureInfo.InvariantCulture to consitently get the right decimal
                                Calculator calc = new Calculator(float.Parse(list[i - 1], CultureInfo.InvariantCulture), float.Parse(list[i + 1], CultureInfo.InvariantCulture));
                                //remove from list old already calced nums and opperators since it calcs using 3 values where opperator is orignal we remove front and back
                                list.RemoveAt(i + 1);
                                list.RemoveAt(i);
                                list.RemoveAt(i - 1);

                                //insert at left nums pose to make our list right also calc our nums with right opperator and replace if float , with . to make it reusable.//
                                list.Insert(i - 1, calc.multiplication().ToString().Replace(",", "."));
                            }

                            //error to catch and also add 2 extra items to list since we delete 2 items at the end of the code
                            catch (FormatException) { list.Clear(); list.AddRange(new string[] { "noob", "error: Formating issue!", "noob" }); ; break; }

                            for (int j = 0; rightParentesesIndex.Count() > j; j++)
                            {
                                if (i < rightParentesesIndex[j])
                                {
                                    rightParentesesIndex[j] -= 2;
                                }
                            }
                            for (int j = 0; leftParantesesIndex.Count() > j; j++)
                            {
                                if (i < leftParantesesIndex[j])
                                {
                                    leftParantesesIndex[j] -= 2;
                                }
                            }
                            i = leftParantesesIndex[countRightParanteseIndex] + 1;
                        }
                    }

                    //if just num pass and keep going
                    else { i++; }

                }

                //if we have done our first pass through we now want treat the other to "+ -"
                else if (!firstpassthrough)
                {
                    //here is our actual loop update
                    if (i == rightParentesesIndex[countRightParanteseIndex])
                    {
                        //uppdate start and enpoint of our list aka increase index value of our list of parantheis.
                        countLeftParanteseIndex++;
                        countRightParanteseIndex++;
                        //to know when to end
                        timestocalced++;
                        //condition to end our loop is that we have calculated all the paranthesis avalialble and since we add a paranthesis with our code it will effectiley have calced it all
                        if (timestocalced == leftParantesesIndex.Count()) { break; }
                        else
                        {
                            i = leftParantesesIndex[countLeftParanteseIndex]+1;
                            firstpassthrough = true;
                        }

                    }
                    //same process see line 259 we just use + as our opperator
                    else if (list[i] == "+")
                    {
                        if (list[i + 1] == "(" || list[i - 1] == ")")
                        {
                            list.RemoveAt(i + 1);
                            for (int j = 0; rightParentesesIndex.Count() > j; j++)
                            {
                                if (i + 1 < rightParentesesIndex[j])
                                {
                                    rightParentesesIndex[j] -= 1;
                                }
                            }
                            for (int j = 0; leftParantesesIndex.Count() > j; j++)
                            {
                                if (i + 1 < leftParantesesIndex[j])
                                {
                                    leftParantesesIndex[j] -= 1;
                                }
                            }
                        }
                        else {}

                        if (list[i - 1] == ")" || list[i - 1] == "(")
                        {

                            list.RemoveAt(i - 1);
                            for (int j = 0; rightParentesesIndex.Count() > j; j++)
                            {
                                if (i - 1 < rightParentesesIndex[j])
                                {
                                    rightParentesesIndex[j] -= 1;
                                }
                            }
                            for (int j = 0; leftParantesesIndex.Count() > j; j++)
                            {
                                if (i - 1 < leftParantesesIndex[j])
                                {
                                    leftParantesesIndex[j] -= 1;
                                }
                            }
                            i--;
                        }

                        //handel poor user input for ex 8**8
                        try
                        {
                            //Constructor where we pase our values but converted from string to int where we use CultureInfo.InvariantCulture to consitently get the right decimal
                            Calculator calc = new Calculator(float.Parse(list[i - 1], CultureInfo.InvariantCulture), float.Parse(list[i + 1], CultureInfo.InvariantCulture));
                            //remove from list old already calced nums and opperators since it calcs using 3 values where opperator is orignal we remove front and back
                            list.RemoveAt(i + 1);
                            list.RemoveAt(i);
                            list.RemoveAt(i - 1);

                            //insert at left nums pose to make our list right also calc our nums with right opperator and replace if float , with . to make it reusable.//
                            list.Insert(i - 1, calc.addition().ToString().Replace(",", "."));
                        }

                        //error to catch and also add 2 extra items to list since we delete 2 items at the end of the code
                        catch (FormatException) { list.Clear(); list.AddRange(new string[] { "noob", "error: Formating issue! ", "noob" }); ; break; }

                        for (int j = 0; rightParentesesIndex.Count() > j; j++)
                        {
                            if (i < rightParentesesIndex[j])
                            {
                                rightParentesesIndex[j] -= 2;
                            }
                        }
                        for (int j = 0; leftParantesesIndex.Count() > j; j++)
                        {
                            if (i < leftParantesesIndex[j])
                            {
                                leftParantesesIndex[j] -= 2;
                            }
                        }
                        i = leftParantesesIndex[countRightParanteseIndex]+1;
                    }

                    //Same process as line 259 so comments will just be a pointer to its comments
                    else if (list[i] == "-")
                    {
                        if (list[i + 1] == "(" || list[i - 1] == ")")
                        {
                            list.RemoveAt(i + 1);
                            for (int j = 0; rightParentesesIndex.Count() > j; j++)
                            {
                                if (i + 1 < rightParentesesIndex[j])
                                {
                                    rightParentesesIndex[j] -= 1;
                                }
                            }
                            for (int j = 0; leftParantesesIndex.Count() > j; j++)
                            {
                                if (i + 1 < leftParantesesIndex[j])
                                {
                                    leftParantesesIndex[j] -= 1;
                                }
                            }
                        }

                        if (list[i - 1] == ")" || list[i - 1] == "(")
                        {
                            list.RemoveAt(i - 1);
                            for (int j = 0; rightParentesesIndex.Count() > j; j++)
                            {
                                if (i - 1 < rightParentesesIndex[j])
                                {
                                    rightParentesesIndex[j] -= 1;
                                }
                            }
                            for (int j = 0; leftParantesesIndex.Count() > j; j++)
                            {
                                if (i - 1 < leftParantesesIndex[j])
                                {
                                    leftParantesesIndex[j] -= 1;
                                }
                            }
                            i--;
                        }

                        //handel poor user input for ex 8**8
                        try
                        {
                            //Constructor where we pase our values but converted from string to int where we use CultureInfo.InvariantCulture to consitently get the right decimal
                            Calculator calc = new Calculator(float.Parse(list[i - 1], CultureInfo.InvariantCulture), float.Parse(list[i + 1], CultureInfo.InvariantCulture));
                            //remove from list old already calced nums and opperators since it calcs using 3 values where opperator is orignal we remove front and back
                            list.RemoveAt(i + 1);
                            list.RemoveAt(i);
                            list.RemoveAt(i - 1);

                            //insert at left nums pose to make our list right also calc our nums with right opperator and replace if float , with . to make it reusable.//
                            list.Insert(i - 1, calc.subtraktion().ToString().Replace(",", "."));
                        }

                        //error to catch and also add 2 extra items to list since we delete 2 items at the end of the code
                        catch (FormatException) { list.Clear(); list.AddRange(new string[] { "noob", "error: Formating issue!", "noob" }); ; break; }

                        for (int j = 0; rightParentesesIndex.Count() > j; j++)
                        {
                            if (i < rightParentesesIndex[j])
                            {
                                rightParentesesIndex[j] -= 2;
                            }
                        }
                        for (int j = 0; leftParantesesIndex.Count() > j; j++)
                        {
                            if (i < leftParantesesIndex[j])
                            {
                                leftParantesesIndex[j] -= 2;
                            }
                        }
                        i = leftParantesesIndex[countRightParanteseIndex];
                    }

                    else { i++; }
                }
            }

            //remove starting left paranthesis 
            list.RemoveAt(0);

            //remove starting right paranthis
            list.RemoveAt(1);

            //from our now list of elemetn 1 join all elements this 1 element into a string
            string output = string.Join("",list);

            //return our value
            return output;
        }
    }
}
