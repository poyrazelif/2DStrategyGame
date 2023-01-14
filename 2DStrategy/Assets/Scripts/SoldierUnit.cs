using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;
using Toolbox;
using DG.Tweening;


public class SoldierUnit : Product
{
    public Tilemap tilemap;
    public List<Vector3> wayPoints = new List<Vector3>();
    private List<Animator> animatorList = new List<Animator>();
    public LineRenderer linePath;
    private bool isTurnedLeft=true;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            animatorList.Add(transform.GetChild(i).GetComponent<Animator>());
        }
    }

    private void OnEnable()
    {
        tilemap = BuildingSystem.Instance.Tilemap;
    }

    public void GoPath(Vector3 TargetPosition)
    {
        TargetPosition.z = -1;
        
        if (TargetPosition.x > transform.position.x && isTurnedLeft) { TurnRight();}
        else if(TargetPosition.x<=transform.position.x && !isTurnedLeft){ TurnLeft();}
        
        PlayAnim("Walk");
     
        wayPoints = AStar.FindPathClosest(tilemap, transform.position, TargetPosition);
        if (wayPoints != null)
        {
            linePath.positionCount = wayPoints.Count;
            linePath.SetPositions(wayPoints.ToArray());
        }

        foreach (Vector3 point in wayPoints)
        {
            transform.DOMove(new Vector3(point.x,point.y,-1), 2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                PlayAnim("Idle");
            });
        }

    }

    public void PlayAnim(string name)
    {
        foreach (var animator in animatorList)
        {
            animator.SetTrigger(name);
        }
    }

    public void TurnRight()
    {
        transform.localScale= new Vector2(-transform.localScale.x, transform.localScale.y);
        isTurnedLeft = false;
    }
    
    public void TurnLeft()
    {
        transform.localScale= new Vector2(-transform.localScale.x, transform.localScale.y);
        isTurnedLeft = true;
    }

}
