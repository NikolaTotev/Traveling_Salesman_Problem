using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ProgramManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TownGameObject;
    public Traveller traveller;
    public Visualizer PathVisualizer;
    public int NumberOfPointsToVisit = 20;
    public int NumberOfParents= 20;

    void Start()
    {
        traveller = new Traveller(NumberOfPointsToVisit, NumberOfParents);
        VisualizeTowns();
    }

    // Update is called once per frame
    void Update()
    {
        traveller.Evolve();
        VisualizeRoute();
    }

    void VisualizeTowns()
    {
        List<Vector3Int> vectorizedPoints = new List<Vector3Int>();
        foreach (Point point in traveller.pointsToVisit)
        {
            vectorizedPoints.Add(new Vector3Int(point.X, 1, point.Y));
        }

        foreach (Vector3Int vector3Int in vectorizedPoints)
        {
            GameObject town = Instantiate(TownGameObject, vector3Int, Quaternion.identity);
        }
    }


    void VisualizeRoute()
    {
        List<Vector3Int> vectorizedPoints = new List<Vector3Int>();
        foreach (Point point in traveller.pointsToVisit)
        {
            vectorizedPoints.Add(new Vector3Int(point.X, 1, point.Y));
        }
        PathVisualizer.UpdatePointList(traveller.currentBest.Path, vectorizedPoints);
    }
}
