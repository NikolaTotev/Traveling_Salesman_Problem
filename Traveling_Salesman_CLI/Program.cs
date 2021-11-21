using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Traveling_Salesman_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Specimen allTimeBest = new Specimen(0, new List<PointF>(), new List<int>(), 0);
            Traveller traveller = new Traveller();
            bool allTimeBestSet = false;
            long attemptCounter = 0;
            Console.CancelKeyPress += delegate {

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Stopping code");
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
            };
            while (true)
            {
                attemptCounter++;
                traveller = new Traveller();
                traveller.Evolve();
                Console.WriteLine("===============================================================");
                Console.WriteLine("========================== NEW TEST =========================== ");
                Console.WriteLine("===============================================================");
                if (traveller.currentBest.FitnessLevel < allTimeBest.FitnessLevel || !allTimeBestSet)
                {
                    allTimeBest = traveller.currentBest;
                    allTimeBestSet = true;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
