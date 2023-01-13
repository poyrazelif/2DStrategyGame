using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public GameObject SelectedObject;

   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         Vector2 raycastPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
         RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);
         
         if (hit.collider!=null)
         {
            SelectedObject =hit.transform.gameObject;
         }
      }

      if (Input.GetMouseButtonDown(1) && SelectedObject.tag=="Walkable")
      {
         SelectedObject.GetComponent<SoldierUnit>().GoPath(Camera.main.ScreenToWorldPoint(Input.mousePosition));
      }
   }

   public Vector3 GetMouseWorldPosition()
   {
      Vector2 raycastPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
      
      RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);
      
      if (hit.collider!=null)
      {
         return hit.point;
      }
      else
      {
         return Vector3.zero;
      }
   }
}
