using System;
using System.Threading;

namespace Traveling_Salesman_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Traveller traveller = new Traveller();
                traveller.Evolve();
                Console.WriteLine("===============================================================");
                Console.WriteLine("========================== NEW TEST =========================== ");
                Console.WriteLine("===============================================================");
                Thread.Sleep(1000);
            }
            



        }
    }
}
