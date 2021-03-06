using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Random = System.Random;
public enum MutationType
{
    swap,
    insert,
    reverse
}
public class Specimen
{
    public List<int> Path;
    public double FitnessLevel = 0;
    public bool FitnessLevelSet = false;
    public List<Point> m_Points;

    public Specimen(int numberOfPoints, List<Point> points, List<int> path)
    {
        Path = path;
        m_Points = points;
    }
    
    public void FitnessFunction()
    {
        for (int i = 0; i < m_Points.Count - 1; i++)
        {
            FitnessLevel += EucDistance(m_Points[Path[i]], m_Points[Path[i + 1]]);
        }
    }

    double EucDistance(Point firstPoint, Point secondPoint)
    {
        int xDiff = firstPoint.X - secondPoint.X;
        int yDiff = firstPoint.Y - secondPoint.Y;
        return Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
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
        Random rand = new System.Random();

        for (int i = 0; i < Path.Count; i++)
        {
            int firstRandIndex = rand.Next(0, Path.Count);
            int secondRandIndex = rand.Next(0, Path.Count);
            SwapMutation(firstRandIndex, secondRandIndex);
        }
    }


    void Reproduce()
    {

    }
}
