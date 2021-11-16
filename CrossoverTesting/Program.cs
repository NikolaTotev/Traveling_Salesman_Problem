using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
            List<int> secondPointList = new List<int>() { 1, 9, 5, 2, 3, 6, 4, 8, 10, 7 };

            int numberOfPoints = 100;
            int numberOfParents = 100;
            Crossover(CrossoverType.twoPoint, firstPointList, secondPointList);
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
            Specimen newSpecimen;
            List<int> firstCrossedList = Enumerable.Repeat(-1, parent1.Count).ToList();
            List<int> secondCrossedList = Enumerable.Repeat(-1, parent2.Count).ToList();
            Random rand = new Random();

            int midpoint = parent1.Count / 2;


            int firstCrossoverIndex = rand.Next(midpoint - midpoint / 2, midpoint);
            int secondCrossoverIndex = rand.Next(midpoint + 1, midpoint + midpoint / 2);

            int indexToCopyFromParent1 = 0;
            int indexToCopyFromParent2 = 0;

            for (int i = firstCrossoverIndex; i <= secondCrossoverIndex; i++)
            {
                firstCrossedList[i] = parent1[i];
                secondCrossedList[i] = parent2[i];
            }

            Console.WriteLine($"Cross over indexes: {firstCrossoverIndex} - {secondCrossoverIndex}");

            for (int i = 0; i < firstCrossedList.Count; i++)
            {
                if (i < firstCrossoverIndex || i > secondCrossoverIndex)
                {
                    bool positionFilledInFirstList = false;
                    bool positionFilledInSecondList = false;

                    while (!positionFilledInFirstList)
                    {
                        if (!firstCrossedList.GetRange(firstCrossoverIndex, secondCrossoverIndex).Contains(parent2[indexToCopyFromParent2]))
                        {
                            firstCrossedList[i] = parent2[indexToCopyFromParent2];
                            positionFilledInFirstList = true;
                            indexToCopyFromParent2++;
                        }
                        else
                        {
                            if (indexToCopyFromParent2 < parent2.Count - 1)
                            {
                                indexToCopyFromParent2++;
                            }
                            else
                            {
                                indexToCopyFromParent2 = 0;
                            }

                        }

                    }

                    while (!positionFilledInSecondList)
                    {
                        if (!secondCrossedList.GetRange(firstCrossoverIndex, secondCrossoverIndex).Contains(parent1[indexToCopyFromParent1]))
                        {
                            secondCrossedList[i] = parent1[indexToCopyFromParent1];
                            positionFilledInSecondList = true;
                            indexToCopyFromParent1++;
                        }
                        else
                        {
                            if (indexToCopyFromParent1 < parent1.Count - 1)
                            {
                                indexToCopyFromParent1++;
                            }
                            else
                            {
                                indexToCopyFromParent1 = 0;
                            }
                        }

                    }
                }
            }

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
