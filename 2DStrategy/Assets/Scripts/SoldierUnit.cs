using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;
using Toolbox;
using DG.Tweening;


public class SoldierUnit : MonoBehaviour
{
    public Tilemap tilemap;
    public List<Vector3> wayPoints = new List<Vector3>();
    public LineRenderer linePath;


    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {

        }
    }

    public void GoPath(Vector3 TargetPosition)
    {
        TargetPosition.z = -1;
     
        wayPoints = AStar.FindPathClosest(tilemap, transform.position, TargetPosition);
        if (wayPoints != null)
        {
            linePath.positionCount = wayPoints.Count;
            linePath.SetPositions(wayPoints.ToArray());
        }

        foreach (Vector3 point in wayPoints)
        {
            transform.DOMove(new Vector3(point.x,point.y,-1), 2f).SetEase(Ease.Linear);
        }

    }
   
}
