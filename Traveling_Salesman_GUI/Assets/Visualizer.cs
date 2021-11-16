using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    

    public LineRenderer lineRenderer;
    public int vertexCount = 12;
    List<Vector3> pointList = new List<Vector3>();

    public Visualizer()
    {
        lineRenderer = new LineRenderer();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePointList(List<int> pointOrder, List<Vector3Int> pointsToVisit)
    {
        pointList.Clear();
        
        for (int i = 0; i < pointOrder.Count; i++)
        {
            pointList.Add(pointsToVisit[pointOrder[i]]);
        }

        lineRenderer.startWidth = 4f;
        lineRenderer.endWidth = 4f;
        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
    }
}
