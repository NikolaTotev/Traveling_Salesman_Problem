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
    private int numberOfPoints = 100;
    public int numberOfParents = 4;
    public CrossoverType currentCrossoverType = CrossoverType.twoPoint;
    public GameObject TownGameObject;
    private bool pointsDrawn;
    private List<Specimen> CurrentGeneration = new List<Specimen>();
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
            Specimen newSpecimen = new Specimen(numberOfPoints);
            newSpecimen.InitializationRandomSwap();
        }
    }

    void Crossover(CrossoverType crossover, Specimen parent1, Specimen parent2)
    {
        switch (crossover)
        {
            case CrossoverType.onePoint:
                OnePointCrossover(parent1,parent1);
                break;
            case CrossoverType.twoPoint:
                TwoPointCrossover(parent1,parent2);
                break;
            case CrossoverType.partialMap:
                PartialMapCrossover(parent1, parent2);
                break;
            case CrossoverType.cyclic:
                CyclicCrossover(parent1, parent2);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(crossover), crossover, null);
        }
    }

    private void OnePointCrossover(Specimen parent1, Specimen parent2)
    {

    }

    private void TwoPointCrossover(Specimen parent1, Specimen parent2)
    {

    }

    private void PartialMapCrossover(Specimen parent1, Specimen parent2)
    {

    }

    private void CyclicCrossover(Specimen parent1, Specimen parent2)
    {

    }

    void EvaluateGeneration()
    {
        foreach (Specimen specimen in CurrentGeneration)
        {
            specimen.Evaluate();
        }

        CurrentGeneration = (from specimen in CurrentGeneration orderby specimen.fitnessLevel select specimen).ToList();
    }

    void Reproduce()
    {
        Crossover(currentCrossoverType, CurrentGeneration[firstPair.Item1], CurrentGeneration[firstPair.Item2]);
        Crossover(currentCrossoverType, CurrentGeneration[secondPair.Item1], CurrentGeneration[secondPair.Item2]);
    }

    void Mutation()
    {
    }

    //Pairing is done on a "top 3 basis"
    void PairParents()
    {
        firstPair = new Tuple<int, int>(CurrentGeneration.Count - 2, CurrentGeneration.Count - 1);
        secondPair = new Tuple<int, int>(CurrentGeneration.Count - 1, CurrentGeneration.Count - 3);
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

