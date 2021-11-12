using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
enum MutationType
{
    swap,
    insert,
    reverse
}

public class Specimen : MonoBehaviour
{
    private List<int> path;
    public int fitnessLevel = 0;
    private List<Vector3Int> m_Points;

    public Specimen(int numberOfPoints, List<Vector3Int> points)
    {
        path = new List<int>();
        for (int i = 0; i < numberOfPoints; i++)
        {
            path.Add(i+1);
        }

        m_Points = points;
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
