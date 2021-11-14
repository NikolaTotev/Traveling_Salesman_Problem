using System;
using System.Collections.Generic;
using System.Drawing;

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
        }

        Specimen Crossover(CrossoverType crossover, Specimen parent1, Specimen parent2)
        {
            Specimen crossoverResult;
            switch (crossover)
            {
                case CrossoverType.onePoint:
                    crossoverResult = OnePointCrossover(parent1, parent1);
                    break;
                case CrossoverType.twoPoint:
                    crossoverResult = TwoPointCrossover(parent1, parent2);
                    break;
                case CrossoverType.partialMap:
                    crossoverResult = PartialMapCrossover(parent1, parent2);
                    break;
                case CrossoverType.cyclic:
                    crossoverResult = CyclicCrossover(parent1, parent2);
                    break;
                default:
                    crossoverResult = null;
                    break;
            }

            return crossoverResult;
        }

        private Specimen OnePointCrossover(Specimen parent1, Specimen parent2)
        {
            return null;
        }

        private Specimen TwoPointCrossover(Specimen parent1, Specimen parent2)
        {
            Specimen newSpecimen;
            List<int> firstCrossedList = parent1.Path;
            List<int> secondCrossedList = parent2.Path;
            Random rand = new Random();
            int midpoint = parent1.Path.Count / 2;


            int firstCrossoverIndex = rand.Next(midpoint - midpoint / 2, midpoint);
            int secondCrossoverIndex = rand.Next(midpoint+1, midpoint + midpoint / 2);

            for (int i = 0; i < firstCrossedList.Count; i++)
            {
                if (i < firstCrossoverIndex || i > secondCrossoverIndex)
                {
                    firstCrossedList[i] = parent2.Path[i];
                    secondCrossedList[i] = parent1.Path[i];
                }
            }
        }

        private Specimen PartialMapCrossover(Specimen parent1, Specimen parent2)
        {
            return null;
        }

        private Specimen CyclicCrossover(Specimen parent1, Specimen parent2)
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
            (path[index1], path[index2]) = (path[index2], path[index1]);
        }

        void InsertMutation(int index, int value)
        {
            path.Insert(index, value);
        }

        void ReverseMutation()
        {

        }

        public void InitializationRandomSwap()
        {
            Random milRand = new System.Random();
            for (int i = 0; i < path.Count; i++)
            {
                SwapMutation(milRand.Next(0, path.Count), milRand.Next(0, path.Count));
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
