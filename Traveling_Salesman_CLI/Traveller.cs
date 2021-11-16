using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Traveling_Salesman_CLI
{
    public enum CrossoverType
    {
        onePoint,
        twoPoint,
        partialMap,
        cyclic
    }

    class Traveller
    {
        private List<Point> pointsToVisit = new List<Point>();

        public int numberOfPoints = 20;
        public int numberOfParents = 20;

        public CrossoverType currentCrossoverType = CrossoverType.twoPoint;
        //public GameObject TownGameObject;
        private bool pointsDrawn;
        private List<Specimen> CurrentGeneration = new List<Specimen>();
        private List<Specimen> NewGeneration = new List<Specimen>();
        private Specimen currentBest;
        Tuple<int, int> firstPair = new Tuple<int, int>(0, 0);
        Tuple<int, int> secondPair = new Tuple<int, int>(0, 0);

        private int generationsSinceLastMin = 0;

        private int numberOfGenerations = 0;
        // Start is called before the first frame update
        //void Start()
        //{
        //    GeneratePoints();
        //    CreateFirstGeneration();
        //    EvaluateGeneration();
        //}

        public Traveller()
        {
            GeneratePoints();
            CreateFirstGeneration();
            currentBest = new Specimen(0, pointsToVisit, null);
            currentBest.FitnessLevelSet = false;
            EvaluateGeneration();
        }


        public void Evolve()
        {

            while (generationsSinceLastMin < 20 && numberOfGenerations < 5000)
            {
                PairParents();
                Mutation();
                EvaluateGeneration();
                VisualizeRoute(currentBest);
                generationsSinceLastMin++;
                numberOfGenerations++;

                Console.WriteLine($"Current Generation: {numberOfGenerations}");
                Console.WriteLine($"Generations since last new min: {generationsSinceLastMin}");
                Console.WriteLine($"Best fitness level: {currentBest.FitnessLevel}");

                for (int i = 0; i < currentBest.Path.Count; i++)
                {
                    Console.Write($"{currentBest.Path[i]} ");
                }
                Console.WriteLine("=============================================================");
                Console.WriteLine("=============================================================");
                Thread.Sleep(100);
            }
        }

        void CreateFirstGeneration()
        {
            for (int i = 0; i < numberOfParents; i++)
            {
                Specimen newSpecimen = new Specimen(numberOfPoints, pointsToVisit, Enumerable.Range(0, numberOfParents).ToList());
                newSpecimen.InitializationRandomSwap();
                CurrentGeneration.Add(newSpecimen);
            }
        }

        void Crossover(CrossoverType crossover, Specimen parent1, Specimen parent2)
        {
            Specimen crossoverResult;
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
                default:
                    break;
            }

        }

        private Specimen OnePointCrossover(Specimen parent1, Specimen parent2)
        {
            return null;
        }

        private void TwoPointCrossover(Specimen parent1, Specimen parent2)
        {
            List<int> firstCrossedList = Enumerable.Repeat(-1, parent1.Path.Count).ToList();
            List<int> secondCrossedList = Enumerable.Repeat(-1, parent2.Path.Count).ToList();
            Random rand = new Random();

            int midpoint = parent1.Path.Count / 2;


            int firstCrossoverIndex = rand.Next(midpoint - midpoint / 2, midpoint);
            int secondCrossoverIndex = rand.Next(midpoint + 1, midpoint + midpoint / 2);

            int indexToCopyFromParent1 = 0;
            int indexToCopyFromParent2 = 0;

            for (int i = firstCrossoverIndex; i <= secondCrossoverIndex; i++)
            {
                firstCrossedList[i] = parent1.Path[i];
                secondCrossedList[i] = parent2.Path[i];
            }

            //Console.WriteLine($"Cross over indexes: {firstCrossoverIndex} - {secondCrossoverIndex}");

            for (int i = 0; i < firstCrossedList.Count; i++)
            {
                if (i < firstCrossoverIndex || i > secondCrossoverIndex)
                {
                    bool positionFilledInFirstList = false;
                    bool positionFilledInSecondList = false;

                    while (!positionFilledInFirstList)
                    {
                        if (!firstCrossedList.GetRange(firstCrossoverIndex, secondCrossoverIndex-firstCrossoverIndex).
                            Contains(parent2.Path[indexToCopyFromParent2]))
                        {
                            if (!firstCrossedList.Contains(parent2.Path[indexToCopyFromParent2]))
                            {
                                firstCrossedList[i] = parent2.Path[indexToCopyFromParent2];
                                positionFilledInFirstList = true;
                            }

                            if (indexToCopyFromParent2 < parent2.Path.Count - 1)
                            {
                                indexToCopyFromParent2++;
                            }
                            else
                            {
                                indexToCopyFromParent2 = 0;
                            }
                        }
                        else
                        {
                            if (indexToCopyFromParent2 < parent2.Path.Count - 1)
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
                        if (!secondCrossedList.GetRange(firstCrossoverIndex, secondCrossoverIndex-firstCrossoverIndex).Contains(parent1.Path[indexToCopyFromParent1]))
                        {
                            if (!secondCrossedList.Contains(parent1.Path[indexToCopyFromParent1]))
                            {
                                secondCrossedList[i] = parent1.Path[indexToCopyFromParent1];
                                positionFilledInSecondList = true;
                            }
                            
                            if (indexToCopyFromParent1 < parent1.Path.Count - 1)
                            {
                                indexToCopyFromParent1++;
                            }
                            else
                            {
                                indexToCopyFromParent1 = 0;
                            }
                        }
                        else
                        {
                            if (indexToCopyFromParent1 < parent1.Path.Count - 1)
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

            //for (int i = 0; i < firstCrossedList.Count; i++)
            //{
            //    Console.Write($"{firstCrossedList[i]} ");
            //}
            //Console.WriteLine();
            //for (int i = 0; i < secondCrossedList.Count; i++)
            //{
            //    Console.Write($"{secondCrossedList[i]} ");
            //}

            Specimen firstChild = new Specimen(numberOfPoints, pointsToVisit, firstCrossedList);
            Specimen secondChild = new Specimen(numberOfPoints, pointsToVisit, secondCrossedList);
            NewGeneration.Add(firstChild);
            NewGeneration.Add(secondChild);
        }

        private Specimen PartialMapCrossover(Specimen parent1, Specimen parent2)
        {
            return null;
        }

        private Specimen CyclicCrossover(Specimen parent1, Specimen parent2)
        {
            return null;
        }

        void EvaluateGeneration()
        {
            foreach (Specimen specimen in CurrentGeneration)
            {
                specimen.FitnessFunction();
            }

            CurrentGeneration = (from specimen in CurrentGeneration orderby specimen.FitnessLevel select specimen).ToList();
            if (!currentBest.FitnessLevelSet)
            {
                currentBest = new Specimen(numberOfPoints, pointsToVisit, CurrentGeneration[0].Path);
                currentBest.FitnessLevelSet = true;
                currentBest.FitnessLevel = CurrentGeneration[0].FitnessLevel;
            }

            if (CurrentGeneration[0].FitnessLevel < currentBest.FitnessLevel)
            {
                currentBest = new Specimen(numberOfPoints, pointsToVisit, CurrentGeneration[0].Path);
                currentBest.FitnessLevelSet = true;
                currentBest.FitnessLevel = CurrentGeneration[0].FitnessLevel;
                generationsSinceLastMin = 0;
            }
        }

        void Mutation()
        {

        }

        void PairParents()
        {
            //Transfer top 3 over to new generation, remaining numberOfParents-3 is considered the "new generation"
            for (int i = 0; i < 3; i++)
            {
                NewGeneration.Add(CurrentGeneration[i]);
            }

            ValueTuple<int, int, int> requiredChildrenCounts;

            int remainingFreeSpace = numberOfParents - 3;

            //ToDo this needs to be verified that it sums up to the correct final value after the new generation is created.
            int fiftyPercent = 50 / 100;
            requiredChildrenCounts.Item1 = (int)(remainingFreeSpace * (50f / 100f));
            requiredChildrenCounts.Item2 = (int)(remainingFreeSpace * (20f / 100f));
            requiredChildrenCounts.Item3 = (int)(remainingFreeSpace * (30f / 100f));

            int estimatedRequiredChildren = requiredChildrenCounts.Item1 + requiredChildrenCounts.Item2 +
                                            requiredChildrenCounts.Item3;
            if (estimatedRequiredChildren != remainingFreeSpace)
            {
                requiredChildrenCounts.Item1 += remainingFreeSpace - estimatedRequiredChildren;
            }
            //Top 0-20% with 30-40% -> 50% of the new generation
            ValueTuple<int, int> firstParentSelectionRange = PercentToIndex(0, 20);
            ValueTuple<int, int> secondParentSelectionRange = PercentToIndex(30, 40);

            Reproduce(requiredChildrenCounts.Item1, firstParentSelectionRange, secondParentSelectionRange);

            //Top 0-20% with bottom 0-20% (80-90%); -> 20% of the new generation
            firstParentSelectionRange = PercentToIndex(0, 20);
            secondParentSelectionRange = PercentToIndex(80, 100);

            Reproduce(requiredChildrenCounts.Item2, firstParentSelectionRange, secondParentSelectionRange);

            //Randomly mix all specimens in the range 30-60%  -> 30% of the new generation
            firstParentSelectionRange = PercentToIndex(30, 50);
            secondParentSelectionRange = PercentToIndex(40, 60);

            Reproduce(requiredChildrenCounts.Item3, firstParentSelectionRange, secondParentSelectionRange);
            CurrentGeneration.Clear();
            foreach (var specimen in NewGeneration)
            {
                CurrentGeneration.Add(new Specimen(numberOfParents, specimen.m_Points, specimen.Path));
            }
        }

        private void Reproduce(int numberOfChildren, ValueTuple<int, int> firstParentSelectionRange, ValueTuple<int, int> secondParentSelectionRange)
        {
            Random randSelector = new Random();

            int firstParentSelectionRangeStart = firstParentSelectionRange.Item1;
            int firstParentSelectionRangeEnd = firstParentSelectionRange.Item2;

            if (firstParentSelectionRange.Item2 < firstParentSelectionRange.Item1)
            {
                firstParentSelectionRangeStart = firstParentSelectionRange.Item2;
                firstParentSelectionRangeEnd = firstParentSelectionRange.Item1;
            }

            int secondParentSelectionRangeStart = secondParentSelectionRange.Item1;
            int secondParentSelectionRangeEnd = secondParentSelectionRange.Item2;

            if (secondParentSelectionRange.Item2 < secondParentSelectionRange.Item1)
            {
                secondParentSelectionRangeStart = secondParentSelectionRange.Item2;
                secondParentSelectionRangeEnd = secondParentSelectionRange.Item1;
            }



            for (int i = 0; i < numberOfChildren; i++)
            {
                Specimen firstParent =
                    CurrentGeneration[
                        randSelector.Next(firstParentSelectionRangeStart, firstParentSelectionRangeEnd)];

                Specimen secondParent =
                    CurrentGeneration[
                        randSelector.Next(secondParentSelectionRangeStart, secondParentSelectionRangeEnd)];

                Crossover(CrossoverType.twoPoint, firstParent, secondParent);
            }
        }

        //Returns the end of the percent interval
        ValueTuple<int, int> PercentToIndex(int startPercent, int endPercent)
        {
            ValueTuple<int, int> indexRange;

            indexRange.Item1 = (int)(numberOfParents * ((float)startPercent / 100f));
            //NOTE in the case of 100 parents 10% returns 10, if you want the correct index subtract 1;
            indexRange.Item2 = (int)(numberOfParents * ((float)endPercent / 100f));

            return indexRange;
        }

        void GeneratePoints()
        {
            Random mRand = new Random();

            for (int i = 0; i < numberOfPoints; i++)
            {
                pointsToVisit.Add(new Point(mRand.Next(-1000, 1000), mRand.Next(-1000, 1000)));
            }

            //VisualizeTowns();
        }

        //void VisualizeTowns()
        //{
        //    foreach (Vector3Int vector3Int in pointsToVisit)
        //    {
        //        GameObject town = Instantiate(TownGameObject, vector3Int, Quaternion.identity);
        //    }
        //}


        void VisualizeRoute(Specimen specimen)
        {

        }
    }
}
