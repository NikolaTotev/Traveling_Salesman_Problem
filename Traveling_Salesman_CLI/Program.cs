using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Traveling_Salesman_CLI
{
    class Program
    {
        static Specimen allTimeBest = new Specimen(0, new List<PointF>(), new List<int>(), 0);
        static Traveller traveller = new Traveller();
        static bool allTimeBestSet = false;
        static long attemptCounter = 0;
        static int maxAttempts = 0;

        static void Main(string[] args)
        {
            Console.CancelKeyPress += delegate
            {

                PrintResults();
            };

            bool inputIsOk = false;

            while (!inputIsOk)
            {
                Console.WriteLine("How many attempts do you want me to try?");
                try
                {
                    maxAttempts = int.Parse(Console.ReadLine());
                    inputIsOk = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Wrong input!");
                    inputIsOk = false;
                }
            }
            


            while (attemptCounter < maxAttempts)
            {

                attemptCounter++;
                traveller = new Traveller();
                traveller.Evolve();
                Console.WriteLine("===============================================================");
                Console.WriteLine($"==== NEW TEST Attempt {attemptCounter} =======================");
                Console.WriteLine("===============================================================");
                if (traveller.currentBest.FitnessLevel < allTimeBest.FitnessLevel || !allTimeBestSet)
                {
                    allTimeBest = traveller.currentBest;
                    allTimeBestSet = true;
                }
                Thread.Sleep(1000);
            }

            PrintResults();
            Console.ReadLine();
        }

        static void PrintResults()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Calculations Completed.");
            Console.WriteLine("============================= Results ==================================");

            Console.WriteLine($"Best path length: {allTimeBest.FitnessLevel}");
            Console.WriteLine($"Number of attempts: {attemptCounter}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Path in index form");
            foreach (var i in allTimeBest.Path)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Path in town name form:");
            for (int i = 0; i < traveller.hardcoded.Count; i++)
            {
                Console.Write($"{traveller.hardcoded[allTimeBest.Path[i]].Item1} ");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("========================================================================");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
