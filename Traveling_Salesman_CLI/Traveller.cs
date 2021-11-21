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
        private List<PointF> pointsToVisit = new List<PointF>();

        public int numberOfPoints = 20;
        public int numberOfParents = 60;
        public int maxGensWithNoImprovment = 24;
        public int topNToSave = 1;

        public CrossoverType currentCrossoverType = CrossoverType.twoPoint;
        //public GameObject TownGameObject;
        private bool pointsDrawn;
        private List<Specimen> CurrentGeneration = new List<Specimen>();
        private List<Specimen> NewGeneration = new List<Specimen>();
        public Specimen currentBest;

        private int generationsSinceLastMin = 0;

        private int numberOfGenerations = 0;
        // Start is called before the first frame update
        //void Start()
        //{
        //    GeneratePoints();
        //    CreateFirstGeneration();
        //    EvaluateGeneration();
        //}

        public List<Tuple<string, float, float>> hardcoded = new List<Tuple<string, float, float>>()
        {
            new Tuple<string, float, float>("Aberystwyth", 0.00019f, -0.00028f),
            new Tuple<string, float, float>("Brighton", 383.458f, -0.00060f),
            new Tuple<string, float, float>("Edinburgh", -27.0206f, -282.758f),
            new Tuple<string, float, float>("Exeter", 335.751f, -269.577f),
            new Tuple<string, float, float>("Glasgow", 69.4331f, -246.78f),
            new Tuple<string, float, float>("Inverness", 168.521f, 31.4012f),
            new Tuple<string, float, float>("Liverpool", 320.35f, -160.9f),
            new Tuple<string, float, float>("London", 179.933f, -318.031f),
            new Tuple<string, float, float>("Newcastle", 492.671f, -131.563f),
            new Tuple<string, float, float>("Nottingham", 112.198f, -110.561f),
            new Tuple<string, float, float>("Oxford", 306.32f, -108.09f),
            new Tuple<string, float, float>("Stratford", 217.343f, -447.089f)
        };

        public Traveller()
        {
            GeneratePointsFromList();
            numberOfPoints = hardcoded.Count;
            numberOfParents = hardcoded.Count;
            CreateFirstGeneration();
            currentBest = new Specimen(0, pointsToVisit, new List<int>());
            currentBest.FitnessLevelSet = false;
            EvaluateGeneration();
        }


        public void Evolve()
        {

            while (generationsSinceLastMin < maxGensWithNoImprovment && numberOfGenerations < 5000)
            {
                PairParents();
                Mutation();
                EvaluateGeneration();
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

                for (int i = 0; i < currentBest.Path.Count; i++)
                {
                    Console.Write($"{hardcoded[currentBest.Path[i]].Item1} ");
                }
                Console.WriteLine("=============================================================");
                Console.WriteLine("=============================================================");
                //Thread.Sleep(100);
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
                    TwoPointCrossoverSetup(parent1, parent2);
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

        private void TwoPointCrossoverSetup(Specimen parent1, Specimen parent2)
        {

            Random rand = new Random();

            int randSelection1 = rand.Next(0, parent1.Path.Count);
            int randSelection2 = rand.Next(0, parent1.Path.Count);
            while (randSelection1 == randSelection2 || randSelection1 == 0 || randSelection2 == 0 || randSelection1 == parent1.Path.Count - 1 || randSelection2 == parent1.Path.Count - 1)
            {
                randSelection1 = rand.Next(0, parent1.Path.Count);
                randSelection2 = rand.Next(0, parent1.Path.Count);
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

            List<int> firstCrossedList = TwoPointCross(firstCrossoverIndex, secondCrossoverIndex, parent1.Path, parent2.Path);
            List<int> secondCrossedList = TwoPointCross(firstCrossoverIndex, secondCrossoverIndex, parent2.Path, parent1.Path);

            Specimen firstChild = new Specimen(numberOfPoints, pointsToVisit, firstCrossedList);
            Specimen secondChild = new Specimen(numberOfPoints, pointsToVisit, secondCrossedList);

            int mutationRandom = rand.Next(4242, 424242);

            if (mutationRandom % 42 == 0)
            {
                firstChild.InitializationRandomSwap();
            }

            if (mutationRandom % 10 == 0)
            {
                firstChild.ReverseMutation(firstCrossoverIndex, secondCrossoverIndex);
                secondChild.ReverseMutation(firstCrossoverIndex, secondCrossoverIndex);
            }

            if (mutationRandom % 50 == 0)
            {
                secondChild.SwapMutation(firstCrossoverIndex, secondCrossoverIndex);
            }
            NewGeneration.Add(firstChild);
            NewGeneration.Add(secondChild);
        }

        private List<int> TwoPointCross(int firstCrossoverIndex, int secondCrossoverIndex, List<int> parent1, List<int> parent2)
        {
            int stationarySectionLength = secondCrossoverIndex - firstCrossoverIndex;

            List<int> crossedList = new List<int>();

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

                if (!isInStationarySection)
                {
                    bool positionFilled = false;
                    while (!positionFilled)
                    {
                        int numberToPlace = parent2[copyFromIndex];

                        if (!placedNumbers.Contains(numberToPlace) && !stationarySection.Contains(numberToPlace))
                        {
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
            for (int i = 0; i < topNToSave; i++)
            {
                NewGeneration.Add(new Specimen(numberOfPoints, CurrentGeneration[i].m_Points, CurrentGeneration[i].Path));
            }

            ValueTuple<int, int, int> requiredChildrenCounts;

            int remainingFreeSpace = numberOfParents - topNToSave;

            //ToDo this needs to be verified that it sums up to the correct final value after the new generation is created.
            requiredChildrenCounts.Item1 = (int)(remainingFreeSpace * (65f / 100f));
            requiredChildrenCounts.Item2 = (int)(remainingFreeSpace * (5f / 100f));
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
            firstParentSelectionRange = PercentToIndex(0, 10);
            secondParentSelectionRange = PercentToIndex(60, 100);

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

        void GeneratePointsFromList()
        {
            foreach (Tuple<string, float, float> tuple in hardcoded)
            {
                pointsToVisit.Add(new PointF(tuple.Item2, tuple.Item3));
            }
        }

    }
}
