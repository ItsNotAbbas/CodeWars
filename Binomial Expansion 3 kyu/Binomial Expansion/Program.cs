using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace BinomialExpansion
{ 
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a binomial in the form (ax+b)^n");
            string expr = Console.ReadLine();
            expr = expr.Trim();

            //Grabs the last character: the exponent then converts it to an integer
            int index = expr.IndexOf('^');
            
            string end_of_expr = expr.Substring(index + 1);
            int exponent = Convert.ToInt32(end_of_expr);
            //Console.WriteLine(exponent);

            List<BigInteger> PascalsRow = Pascal_row(exponent);

            //Console.WriteLine(string.Join(" ", PascalsRow));

            //The possibilities for a variable
            Regex variable_possibilities = new Regex(@"[a-zA-Z]");
            //Check if there are matches (will always be true)
            Match check_match = variable_possibilities.Match(expr);
            //Assign whatever was matched to this variable
            string variable = check_match.Value;

            //To be used later (see Get_terms)
            List<int> given_terms = new List<int>();

            //Assign terms 1 and 2 from binomial to a list
            given_terms = Get_terms(expr, given_terms, variable);

            //Console.WriteLine(string.Join(" ", given_terms));

            string result = Final_calculations(given_terms, variable, exponent, PascalsRow);

            Console.WriteLine(result);

            Console.ReadKey();
        }
        //Function to calculate the row of Pascal's triangle (CAN BE OPTIMISED MASSIVELY, CBA RN THO)
        static List<BigInteger> Pascal_row(int exponent)
        {
            //Pascal's row will be here (adjustments for coefficients not made yet)
            List<BigInteger> PascalsRow = new List<BigInteger>();

            //Declare an initialise numerator out of loop for optimisation: It will not change
            BigInteger numerator = Factorial(exponent);

            for (int i = 0; i <= exponent; i++)
            {
                BigInteger denominator1 = Factorial(i);
                BigInteger denominator2 = Factorial(exponent - i);
                BigInteger denominator = denominator1 * denominator2;
                //Calculate coefficient
                BigInteger coefficient = numerator / denominator; 
                PascalsRow.Add(coefficient);
            }
            return PascalsRow;
        }

        //Function to calculate factorial
        static BigInteger Factorial(int num)
        {
            if (num == 0)
            {
                return 1;
            }
            BigInteger calculated_factorial = 1;
            for (int i = 1; i <= num; i++)
            {
                calculated_factorial *= i;
            }
            return calculated_factorial;
        }
        static List<int> Get_terms(string expr, List<int> given_terms, string variable)
        {

            //Get the index of the variable
            int index_variable = expr.IndexOf(variable);

            //Remove everything unimportant (Only focus on terms and variables). The -1 excludes ')'
            expr = expr.Substring(1, expr.IndexOf(")") - 1);

            //Declare so can be used outside scope of selection statement
            int first_term;

            //Get first coefficient from given binomial and convert to int
            if (expr.Substring(0,1) == variable)
            {
                first_term = 1;
            }
            else
            {
                first_term = int.Parse(expr.Substring(0, index_variable - 1));//The -1 excludes variable
            }

            //Get second term by removing everything up to the variable (inclusive)

            int second_term = int.Parse(expr.Remove(0, index_variable));

            string second_term_str = Convert.ToString(second_term);

            //Remove unnecessary '+' sign if it's there
            if (second_term_str.Contains("+"))
            {
                int.Parse(second_term_str.Replace("+", ""));
            }

            //Add them to their own list
            given_terms.Add(first_term);
            given_terms.Add(second_term);

            return (given_terms);
        }

        static string Final_calculations(List<int> given_terms, string variable, int exponent, List<BigInteger> PascalsRow)
        {
            //These are the terms from the given binomial e.g 2 and 1 in (2x+1)^exp
            int first_term = given_terms[0];
            int second_term = given_terms[1];

            string result = "";
            
            int exponent_secondterm = 0;
            for (int i = 0; i < PascalsRow.Count; i++)
            {
                BigInteger coefficient = PascalsRow[i] * BigInteger.Pow(first_term, exponent) * BigInteger.Pow(second_term, exponent_secondterm);
                //Console.WriteLine(coefficient);

                string coefficient_str = Convert.ToString(coefficient);

                if (coefficient > 0)
                {
                    coefficient_str = $"+{Convert.ToString(coefficient)}";
                }

                result += $"{((exponent == 0) ? coefficient_str : 
                             ((exponent == 1) ? coefficient_str + variable : 
                             coefficient_str + variable +"^" + exponent))}";

                exponent--;
                exponent_secondterm++;
            }

            //Remove '+' from beginning where necessary
            if (result.Substring(0,1) == "+" && result.Length >= 1)
            {
                result = result.Substring(1);
            }

            if (result.Substring(0,2) == $"1{variable} && result.Length >= 2")
            {
                result = result.Substring(1);
            }

            if (result.Contains($"0{variable}"))
            {
                result.Replace($"0{variable}", "");
            }

            return result;
        }
    }   
}