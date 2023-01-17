using System.Collections;
using System.Collections.Generic;
using Toolbox;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FindPath : MonoBehaviour
{
    public Transform endPos;
    public Tilemap tilemap;
    public List<Vector3> wayPoints = new List<Vector3>();
    public LineRenderer linePath;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            endPos.position = mousePos;
            wayPoints = AStar.FindPathClosest(tilemap, transform.position, endPos.position);
            if (wayPoints != null)
            {
                linePath.positionCount = wayPoints.Count;
                linePath.SetPositions(wayPoints.ToArray());
            }
        }
    }
}
