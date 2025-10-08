using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System;

namespace AdvanceTrainingday1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Enter value for A:");
            int A = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter value for B:");
            int B = Convert.ToInt32(Console.ReadLine());

            int C = A + B;
            Console.WriteLine("output of sum : " + C);
            */

            //string username = "swaraj";
            //Console.WriteLine(username);
            //Console.WriteLine(username.Length);

            //string fullname = username.Insert(0, "Mr. ");
            //Console.WriteLine(fullname);



            //string substrng = username.Substring(1);
            //Console.WriteLine(substrng);

            //int age = Convert.ToInt32(Console.ReadLine());

            //if (age > 21)
            //{
            //    Console.WriteLine("you can drink");

            //}
            //else if (age>18)
            //{
            //    Console.WriteLine("you have to bring someone adult with you");

            //}
            //else 
            //{
            //    Console.WriteLine("go home baby");
            //}

            //message = ((temperature <= 15) ? "temp is okay" : "temp is not okay");
            //Console.WriteLine(message);

            //Console.ReadKey();

            /*
            string firstname = "shaan";

            string lastname = "swaraj";

            Console.WriteLine("enter age:");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"my name is {firstname} {lastname} and my age is {age} ");
            */

            //Console.WriteLine("what day it is:");
            //string day = Console.ReadLine();
            //switch (day)
            //{
            //    case "Monday":
            //        Console.WriteLine("okay it is monday");
            //        break;
            //    case "Tuesy":
            //        Console.WriteLine("okay it is tuesday");
            //        break;
            //    default:
            //        Console.WriteLine("Somethign else");
            //        break;
            //}

            //Console.WriteLine("what temp is it like:");
            //string temp = Console.ReadLine();
            //switch (temp)
            //{
            //    case "":
            //        Console.WriteLine("okay it is monday");
            //        break;
            //    case "Tuesy":
            //        Console.WriteLine("okay it is tuesday");
            //        break;
            //    default:
            //        Console.WriteLine("Somethign else");
            //        break;
            //}




            //task 1 -----------------------------------------------------------404 

            /*
             * Description:
            Write a C# program that stores a student’s name and marks for 5 subjects using variables.
            Then calculate and display:
            Total marks
            Average marks
            Percentage
            Steps:
            Declare string studentName and 5 integer variables for marks.
            Calculate total, average, and percentage (assume each subject is 100 marks).
            Print all the results neatly.
            */
            string studentName = "Bruce lee";
            int subject1 = 85;
            int subject2 = 92;
            int subject3 = 78;
            int subject4 = 95;
            int subject5 = 88;

            const int totalPossibleMarks = 500;
            int totalMarks = subject1 + subject2 + subject3 + subject4 + subject5;
            //Console.WriteLine(totalMarks);

            double averagemarks = ((double)totalMarks / 5);
            //Console.WriteLine(averagemarks);

            double percentage = ((double)totalMarks / totalPossibleMarks) * 100;
            //Console.WriteLine(percentage);

            Console.WriteLine($"Student Name: {studentName}");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Subject 1 Mark (Math):     {subject1} / 100");
            Console.WriteLine($"Subject 2 Mark (Science):  {subject2} / 100");
            Console.WriteLine($"Subject 3 Mark (GK):  {subject3} / 100");
            Console.WriteLine($"Subject 4 Mark (English):  {subject4} / 100");
            Console.WriteLine($"Subject 5 Mark (Hindi):      {subject5} / 100");

            Console.WriteLine($"Total Marks Secured:       {totalMarks} / {totalPossibleMarks}");
            Console.WriteLine($"Average Marks:             {averagemarks:F2} (Rounded to 2 decimal places)");
            Console.WriteLine($"Final Percentage:          {percentage:F2}%");

            Console.WriteLine("---------------------------------------------");


            //task II ----------------------------------------------------------------
            /*
            Simple Salary Computation
Description:
Create a program to calculate the net salary of an employee using variables.
Steps:
Declare variables for:
Basic salary
HRA(20 % of basic)
DA(10 % of basic)
Tax(8 % of gross)
Use formulas: gross = basic + HRA + DA
netSalary = gross - tax
Display all values with proper labels.
            */


            string employeename = "RAJU";
            double basicSalary = 50000.00;


            const double HRA_RATE = 0.20;
            const double DA_RATE = 0.10;
            const double TAX_RATE = 0.08;


            double hra = basicSalary * HRA_RATE;
            double da = basicSalary * DA_RATE;


            double grossSalary = basicSalary + hra + da;

            double tax = grossSalary * TAX_RATE;

            double netSalary = grossSalary - tax;

            Console.WriteLine("\n=============================================");
            Console.WriteLine($"EMPLOYEE: {employeename}");

            Console.WriteLine($"1. Basic Salary:     {basicSalary,15:N2}");
            Console.WriteLine($"2. HRA (20.00%):     {hra,15:N2}");
            Console.WriteLine($"3. DA (10.00%):      {da,15:N2}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"GROSS SALARY:        {grossSalary,15:N2}");
            Console.WriteLine($"Tax Deducted (8.00%):{tax,15:N2}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"NET SALARY:          {netSalary,15:N2}");




            // Task III -----------------------------------------------------------------------------------------
            /*
             * Task 3 – Currency Converter
            Description:

            Write a program to convert an amount in INR (Indian Rupees) to USD and EUR using given rates.
            Steps:
            Declare a double inr and two conversion rates (1 USD = 83.0 INR, 1 EUR = 90.5 INR).
            Calculate: usd=inr /83.0

            eur = inr/90.5
            Display the results rounded to two decimals.


             ----------------------------------------------------------------------------------------------------------


            Task 4 – Time Converter
            Description:

            Convert a given time in minutes into hours and minutes using integer variables.
            Steps:
            Input: total minutes (e.g., 130)
            Calculate hours and remaining minutes:

            hours=totalMinutes/60

            minutes =totalminutes %60
            Print result.
             -------------------------------------------------------------------------------------------------------------
             */

            //printing square in nested loop
            //int value = 0;

            Console.WriteLine("--- Square Pattern Generator ---");
            Console.Write("Enter the number of rows : ");

            int rows = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the number of columns : ");
            int columns = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nGenerating a {rows}x{columns} square pattern:");
            Console.WriteLine("---------------------------------------------");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("GOPAL");
                }

                Console.WriteLine();
            }
            //--------------------------------------------------------------------------------------------------
            //*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

            //ARRAYS - thats can store mutiple values.fixed size

            //declare the array


            //string[] bikes = new string[3]; //here we declare the array

            //bikes[0] = "nonsense";     //here we assign the values

            //Console.WriteLine(bikes[1]);

            //Console.ReadLine();

            //  string[] cars = { "BMW", "MUSTANG", "LAMBORGINI" }; //WE JUST ADD BRACKET THAT WE CREATE THE SPACE

            //Console.WriteLine(cars[1]); // here the index startes with the 0 so indexing is already created once the array is created 
            //cars[1] = "skyline";
            //Console.WriteLine(cars[1]);
            //Console.ReadLine();

            //for (int i = 0; i < cars.Length; i++)
            //{
            //    Console.WriteLine(cars[i]);
            //}

            //Console.ReadLine();


            //Console.WriteLine(cars[1]);


///////////////////////////////////////////////////////////////////////////////////////////////

            // FOREACH   LOOPS for aray :- it can ititirate however we want but it is not flexible

            //string[] cars = { "BMW", "MERCE", "AUDI" };

            ////       datatype name in Arrayname
            //foreach (string car in cars)
            //{
            //    Console.WriteLine(car);
            //}


            //Console.ReadLine();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            /*
             * Task 1:Print a table of squares and cubes for numbers 1 to 10.
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            Task2: Find all “perfect numbers” between 1 and 1000 (numbers equal to the sum of their proper divisors).

//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            Task 3:Print this pattern :
            *****
            ***
            *
            ***
            *****

//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
            Task 4:Print this pattern :
            1
            121
            12321
            1234321
            123454321

//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            Task 5:Triangle with alternate numbers:
            1
            0 1
            1 0 1
            0 1 0 1
            1 0 1 0 1

            Task 6:Display all Armstrong numbers between 100 and 999.(Armstrong number⇒sum of each digitn=original number)

                        */
//???????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????
            //task 1

            Console.WriteLine("enter a number:");
            int x = Convert.ToInt32(Console.ReadLine());

            int square = x * x;
            int cube = square * x;
            Console.WriteLine($"square is: {square} and cube is {cube}");
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            //task II - perfect numbers

            const int START_RANGE = 1;
            const int END_RANGE = 1000;
            Console.WriteLine($"Perfect Numbers between {START_RANGE} and {END_RANGE}\n");
            Console.WriteLine("A perfect number is equal to the sum of its proper divisors.");
            Console.WriteLine("Found Perfect Numbers:");

            for (int number = START_RANGE; number <= END_RANGE; number++)
            {

                int sumOfDivisors = 0;

                for (int divisor = 1; divisor <= number / 2; divisor++)
                {
                    if (number % divisor == 0)
                    {
                        sumOfDivisors += divisor;
                    }
                }

                if (sumOfDivisors == number)
                {
                    Console.WriteLine($"-> {number}");
                }
            }

//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //Task III
            /*Task 3:Print this pattern :
            *****
            ***
            *
            ***
            *****

                        */

            const int N = 5;

            Console.WriteLine(" Hourglass Pattern ");

            for (int i = 0; i < N; i++)
            {
                
                int spaces = Math.Min(i, N - 1 - i);

                
                int stars = N - (2 * spaces);

                Console.Write(new string(' ', spaces));
                Console.WriteLine(new string('*', stars));
            }

//#########################################################################################
            //TASK IV

            const int M = 5;

            Console.WriteLine("--- Number Pyramid Pattern ---\n");

            for (int i = 1; i <= M; i++)
            {
                
                for (int j = 0; j < M - i; j++)
                {
                    Console.Write(" ");
                }

                
                for (int j = 1; j <= i; j++)
                {
                    Console.Write(j);
                }

                
                for (int j = i - 1; j >= 1; j--)
                {
                    Console.Write(j);
                }

                Console.WriteLine();
            }

//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            //task V

            //const int N = 5;
            
            Console.WriteLine("Binary Pyramid ");

            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    int value = (i + j) % 2 == 0 ? 1 : 0;
                    Console.Write(value + " ");
                }
                Console.WriteLine();
            }

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //task VI

            Console.WriteLine("Armstrong Numbers:\n");

            for (int i = 100; i <= 999; i++)
            {
                int originalNumber = i;
                int tempNumber = i;
                int sum = 0;

                while (tempNumber > 0)
                {
                    int digit = tempNumber % 10;
                    sum += digit * digit * digit;
                    tempNumber /= 10;
                }

                if (originalNumber == sum)
                {
                    Console.WriteLine(originalNumber);
                }
            }

            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<
            /*
---------------------------------------------------------------------------------------------
Task 7:Print Fibonacci series in reverse order
------------------------------------------------------------------------------------------
Task 8: Zigzag star pattern (height = 4)
   *       *
  * *     * *
*   *   *   *
*     * *     *
------------------------------------------------------------------------------------------
Task 9: Count the total digits in a number using a loop.
------------------------------------------------------------------------------------------
Task 10: Diamond pattern with numbers
       1
      121
    12321
  1234321
123454321
  1234321
    12321
      121
        1
-------------------------------------------------------------------------------------------

            */

            //TASK 7
            int n = 10;
            int[] fibSeries = new int[n];
            int a = 0;
            int b = 1;
            int c;

            fibSeries[0] = a;

            for (int i = 1; i < n; i++)
            {
                fibSeries[i] = b;
                c = a + b;
                a = b;
                b = c;
            }

            for (int i = n - 1; i >= 0; i--)
            {
                Console.Write(fibSeries[i] + " ");
            }

            Console.WriteLine();

            // TASK 8


            //Task 9
            long number;
            int count = 0;

            Console.Write("Enter a number: ");

            if (!long.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            long tempNumber = Math.Abs(number);
            count = tempNumber.ToString().Length;

            Console.WriteLine($"The number {number} has {count} digits.");



            //task 10
            int A = 5;
            int R = 2 * A - 1;

            for (int i = 1; i <= R; i++)
            {
                int k = i <= A ? i : 2 * N - i;

                for (int j = 0; j < A - k; j++)
                {
                    Console.Write(" ");
                }

                for (int j = 1; j <= k; j++)
                {
                    Console.Write(j);
                }

                for (int j = k - 1; j >= 1; j--)
                {
                    Console.Write(j);
                }

                Console.WriteLine();
            }


        }

    }
}
