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
    private bool isTurnedLeft=true;
    private Coroutine followWayCoroutine;

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
        
        if (followWayCoroutine != null)
            StopCoroutine(followWayCoroutine); 

        TargetPosition.z = 0;
        
        if (TargetPosition.x > transform.position.x && isTurnedLeft) { TurnRight();}
        else if(TargetPosition.x<=transform.position.x && !isTurnedLeft){ TurnLeft();}
        
        wayPoints = AStar.FindPathClosest(tilemap, transform.position, TargetPosition);
        
        if (wayPoints != null)
        {
            BuildingSystem.Instance.line.positionCount = wayPoints.Count;
            BuildingSystem.Instance.line.SetPositions(wayPoints.ToArray());
            
            followWayCoroutine= StartCoroutine(CO_FollowWayPoints());
        }
    }

    IEnumerator CO_FollowWayPoints()
    {
        StopAnim("idle");
        PlayAnim("walk");
        
        foreach (Vector3 point in wayPoints)
        {
            
            float waitTime = Vector3.Distance(point , transform.position)*.5f;
            transform.DOMove(new Vector3(point.x, point.y, 0),  waitTime).SetEase(Ease.Linear);
            yield return new WaitForSeconds(waitTime);
        }
        
        StopAnim("walk");
        PlayAnim("idle");
    }

    public void StopAnim(string name)
    {
        foreach (var animator in animatorList)
        {
            animator.SetBool(name,false);
        }
    }

    
    public void PlayAnim(string name)
    {
        foreach (var animator in animatorList)
        {
            animator.SetBool(name,true);
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
