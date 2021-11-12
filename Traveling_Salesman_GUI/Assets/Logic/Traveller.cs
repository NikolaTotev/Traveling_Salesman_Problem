using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;
public class Traveller : MonoBehaviour
{
    private List<Vector3Int> pointsToVisit = new List<Vector3Int>();
    private int numberOfPoints = 100;
    public GameObject TownGameObject;
    enum CrossoverType
    {
        onePoint,
        twoPoint,
        partialMap,
        cyclic
    }

    enum MutationType
    {
        swap,
        insert,
        reverse
    }

    // Start is called before the first frame update
    void Start()
    {
        GeneratePoints();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Crossover(CrossoverType crossover, Specimen parent1, Specimen parent2)
    {
        switch (crossover)
        {
            case CrossoverType.onePoint:
                break;
            case CrossoverType.twoPoint:
                break;
            case CrossoverType.partialMap:
                break;
            case CrossoverType.cyclic:
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

    void Mutate(MutationType mutation, Specimen child)
    {

    }

    void Reproduce()
    {

    }

    void Evaluate()
    {

    }

    void FitnessFunction(Specimen specimen)
    {

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
            Instantiate(TownGameObject, vector3Int, Quaternion.identity);
        }
    }

    void VisualizeRoute(Specimen specimen)
    {

    }
}

