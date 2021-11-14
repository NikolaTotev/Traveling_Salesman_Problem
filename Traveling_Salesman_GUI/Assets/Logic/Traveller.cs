using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;
[Flags]
public enum CrossoverType
{
    onePoint,
    twoPoint,
    partialMap,
    cyclic
}
public class Traveller : MonoBehaviour
{
    private List<Vector3Int> pointsToVisit = new List<Vector3Int>();

    public int numberOfPoints = 100;
    public int numberOfParents = 100;

    public CrossoverType currentCrossoverType = CrossoverType.twoPoint;
    public GameObject TownGameObject;
    private bool pointsDrawn;
    private List<Specimen> CurrentGeneration = new List<Specimen>();
    private List<Specimen> NewGeneration = new List<Specimen>();
    private Specimen currentBest;
    Tuple<int, int> firstPair = new Tuple<int, int>(0, 0);
    Tuple<int, int> secondPair = new Tuple<int, int>(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        GeneratePoints();
        CreateFirstGeneration();
        EvaluateGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        PairParents();
        Reproduce();
        Mutation();
        EvaluateGeneration();
        VisualizeRoute(currentBest);
    }



    void CreateFirstGeneration()
    {
        for (int i = 0; i < 4; i++)
        {
            Specimen newSpecimen = new Specimen(numberOfPoints, pointsToVisit);
            newSpecimen.InitializationRandomSwap();
        }
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

    void EvaluateGeneration()
    {
        foreach (Specimen specimen in CurrentGeneration)
        {
            specimen.Evaluate();
        }

        CurrentGeneration = (from specimen in CurrentGeneration orderby specimen.fitnessLevel select specimen).ToList();
    }

    void Mutation()
    {
    }

    //Pairing is done on a "top 3 basis"
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
        requiredChildrenCounts.Item1 = remainingFreeSpace * (50 / 100);
        requiredChildrenCounts.Item2 = remainingFreeSpace * (20 / 100);
        requiredChildrenCounts.Item3 = remainingFreeSpace * (30 / 100);

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
    }

    private void Reproduce(int numberOfChildren, ValueTuple<int, int> firstParentSelectionRange, ValueTuple<int, int> secondParentSelectionRange)
    {
        Random randSelector = new Random();

        for (int i = 0; i < numberOfChildren; i++)
        {
            Specimen firstParent =
                CurrentGeneration[
                    randSelector.Next(firstParentSelectionRange.Item1, firstParentSelectionRange.Item2)];

            Specimen secondParent =
                CurrentGeneration[
                    randSelector.Next(secondParentSelectionRange.Item1, secondParentSelectionRange.Item2)];

            Specimen child = Crossover(CrossoverType.twoPoint, firstParent, secondParent);
            NewGeneration.Add(child);
        }
    }

    //Returns the end of the percent interval
    ValueTuple<int, int> PercentToIndex(int startPercent, int endPercent)
    {
        ValueTuple<int, int> indexRange;

        indexRange.Item1 = numberOfParents * (startPercent - 10 / 100);
        //NOTE in the case of 100 parents 10% returns 10, if you want the correct index subtract 1;
        indexRange.Item2 = numberOfParents * (endPercent / 100);

        return indexRange;
    }

    void GeneratePoints()
    {
        Random mRand = new Random();

        for (int i = 0; i < numberOfPoints; i++)
        {
            pointsToVisit.Add(new Vector3Int(mRand.Next(-1000, 1000), 0, mRand.Next(-1000, 1000)));
        }

        VisualizeTowns();
    }

    void VisualizeTowns()
    {
        foreach (Vector3Int vector3Int in pointsToVisit)
        {
            GameObject town = Instantiate(TownGameObject, vector3Int, Quaternion.identity);
        }
    }


    void VisualizeRoute(Specimen specimen)
    {

    }
}

