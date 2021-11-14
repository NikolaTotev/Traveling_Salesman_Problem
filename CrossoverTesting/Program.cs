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
            List<Point> pointsToVisit = new List<Point>();

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
            return null;
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
        private List<int> path;
        public int fitnessLevel = 0;
        //private List<Vector3Int> m_Points;

        public Specimen(int numberOfPoints)
        {
            path = new List<int>();
            for (int i = 0; i < numberOfPoints; i++)
            {
                path.Add(i + 1);
            }

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
