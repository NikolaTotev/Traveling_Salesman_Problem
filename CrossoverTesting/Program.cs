using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace CrossoverTesting
{
    public enum CrossoverType
    {
        onePoint,
        twoPoint,
        partialMap,
        cyclic
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Specimen> CurrentGeneration = new List<Specimen>();
            List<int> firstPointList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //List<int> secondPointList = new List<int>() { 1, 9, 5, 2, 3, 6, 4, 8, 10, 7 };
            List<int> secondPointList = new List<int>() { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            int numberOfPoints = 100;
            int numberOfParents = 100;

            while (true)
            {
                Crossover(CrossoverType.twoPoint, firstPointList, secondPointList);
                Console.WriteLine();
                Console.WriteLine("===================================================");
                Console.WriteLine();

                Thread.Sleep(1000);
            }

        }

        static void Crossover(CrossoverType crossover, List<int> parent1, List<int> parent2)
        {
            Console.WriteLine("Parent lists.");

            for (int i = 0; i < parent1.Count; i++)
            {
                Console.Write($"{parent1[i]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < parent2.Count; i++)
            {
                Console.Write($"{parent2[i]} ");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Results from crossing");

            switch (crossover)
            {
                case CrossoverType.onePoint:
                    OnePointCrossover(parent1, parent1);
                    break;
                case CrossoverType.twoPoint:
                    TwoPointCrossover(parent1, parent2);
                    break;
                case CrossoverType.partialMap:
                    PartialMapCrossover(parent1, parent2);
                    break;
                case CrossoverType.cyclic:
                    CyclicCrossover(parent1, parent2);
                    break;
            }
        }

        private static Specimen OnePointCrossover(List<int> parent1, List<int> parent2)
        {
            return null;
        }

        private static void TwoPointCrossover(List<int> parent1, List<int> parent2)
        {
            List<int> firstCrossedList = new List<int>();
            List<int> secondCrossedList = new List<int>();
            Random rand = new Random();

            int midpoint = parent1.Count / 2;


            //int firstCrossoverIndex = rand.Next(midpoint - midpoint / 2, midpoint);
            //int secondCrossoverIndex = rand.Next(midpoint + 1, midpoint + midpoint / 2);

            //int indexToCopyFromParent1 = 0;
            //int indexToCopyFromParent2 = 0;

            //for (int i = firstCrossoverIndex; i <= secondCrossoverIndex; i++)
            //{
            //    firstCrossedList[i] = parent1[i];
            //    secondCrossedList[i] = parent2[i];
            //}
            int randSelection1 = rand.Next(0, parent1.Count);
            int randSelection2 = rand.Next(0, parent1.Count);
            while (randSelection1 == randSelection2 || randSelection1 == 0 || randSelection2 == 0 || randSelection1 == parent1.Count - 1 || randSelection2 == parent1.Count - 1)
            {
                randSelection1 = rand.Next(0, parent1.Count);
                randSelection2 = rand.Next(0, parent1.Count);
            }

            int firstCrossoverIndex = 0;
            int secondCrossoverIndex = 0;

            if (randSelection1 < randSelection2)
            {
                firstCrossoverIndex = randSelection1;
                secondCrossoverIndex = randSelection2;
            }
            else
            {
                firstCrossoverIndex = randSelection2;
                secondCrossoverIndex = randSelection1;
            }


            firstCrossedList = TwoPointCross(firstCrossoverIndex, secondCrossoverIndex, parent1, parent2);
            secondCrossedList = TwoPointCross(firstCrossoverIndex, secondCrossoverIndex, parent2, parent1);


            for (int i = 0; i < firstCrossedList.Count; i++)
            {
                Console.Write($"{firstCrossedList[i]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < secondCrossedList.Count; i++)
            {
                Console.Write($"{secondCrossedList[i]} ");
            }
        }


        private static List<int> TwoPointCross(int firstCrossoverIndex, int secondCrossoverIndex, List<int> parent1, List<int> parent2)
        {
            int stationarySectionLength = secondCrossoverIndex - firstCrossoverIndex;
            //for (int i = firstCrossoverIndex; i <= stationarySectionLength; i++)
            //{
            //    firstCrossedList[i] = parent1[i];
            //    secondCrossedList[i] = parent2[i];
            //}

            List<int> crossedList = new List<int>();
            Console.WriteLine($"Cross over indexes: {firstCrossoverIndex} - {secondCrossoverIndex}");



            foreach (int element in parent1)
            {
                crossedList.Add(element);
            }



            List<int> placedNumbers = new List<int>();
            List<int> stationarySection = parent1.GetRange(firstCrossoverIndex, stationarySectionLength + 1);

            int copyFromIndex;
            int copyToIndex;
            if (secondCrossoverIndex < parent2.Count - 1)
            {
                copyFromIndex = secondCrossoverIndex + 1;
                copyToIndex = secondCrossoverIndex + 1;
            }
            else
            {
                copyFromIndex = 0;
                copyToIndex = 0;
            }


            for (int i = 0; i < parent1.Count; i++)
            {
                bool isInStationarySection = copyToIndex >= firstCrossoverIndex && copyToIndex <= secondCrossoverIndex;

                //Console.WriteLine($"Moving to next");
                if (!isInStationarySection)
                {
                    bool positionFilled = false;
                    while (!positionFilled)
                    {
                        int numberToPlace = parent2[copyFromIndex];
                        //Console.WriteLine();
                        //Console.WriteLine($"Trying to place {numberToPlace} from {copyFromIndex} to {copyToIndex}");
                        if (!placedNumbers.Contains(numberToPlace) && !stationarySection.Contains(numberToPlace))
                        {
                            //Console.WriteLine($"I managed to place {numberToPlace} from {copyFromIndex} into {copyToIndex}");
                            crossedList[copyToIndex] = parent2[copyFromIndex];
                            placedNumbers.Add(numberToPlace);
                            positionFilled = true;

                            if (copyToIndex < parent1.Count - 1)
                            {
                                copyToIndex++;
                            }
                            else
                            {
                                copyToIndex = 0;
                            }
                        }
                        else
                        {
                            //Console.WriteLine($"But I failed because {placedNumbers.Contains(numberToPlace)} or {stationarySection.Contains(numberToPlace)}");
                            if (copyFromIndex < parent2.Count - 1)
                            {
                                copyFromIndex++;
                            }
                            else
                            {
                                copyFromIndex = 0;
                            }
                        }

                    }

                }
                else
                {
                    if (copyToIndex < parent1.Count - 1)
                    {
                        copyToIndex++;
                    }
                    else
                    {
                        copyToIndex = 0;
                    }
                }
            }

            return crossedList;
        }
        private static Specimen PartialMapCrossover(List<int> parent1, List<int> parent2)
        {
            return null;
        }

        private static Specimen CyclicCrossover(List<int> parent1, List<int> parent2)
        {
            return null;
        }
    }

    enum MutationType
    {
        swap,
        insert,
        reverse
    }

    public class Specimen
    {
        public List<int> Path;
        public int fitnessLevel = 0;
        //private List<Vector3Int> m_Points;

        public Specimen(List<int> path)
        {
            Path = path;
            //  m_Points = points;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void FitnessFunction(Specimen specimen)
        {

        }

        void Mutate(MutationType mutation, Specimen child)
        {
            switch (mutation)
            {
                case MutationType.swap:
                    break;
                case MutationType.insert:
                    break;
                case MutationType.reverse:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mutation), mutation, null);
            }
        }

        void SwapMutation(int index1, int index2)
        {
            (Path[index1], Path[index2]) = (Path[index2], Path[index1]);
        }

        void InsertMutation(int index, int value)
        {
            Path.Insert(index, value);
        }

        void ReverseMutation()
        {

        }

        public void InitializationRandomSwap()
        {
            Random milRand = new System.Random();
            for (int i = 0; i < Path.Count; i++)
            {
                SwapMutation(milRand.Next(0, Path.Count), milRand.Next(0, Path.Count));
            }
        }


        void Reproduce()
        {

        }

        public void Evaluate()
        {

        }


    }

}
