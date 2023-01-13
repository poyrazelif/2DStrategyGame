using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public GameObject SelectedObject;
   private Vector3 offset;

   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         Vector2 raycastPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
         RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);
         
         if (hit.collider!=null)
         {
            SelectObject(hit.transform.gameObject);
            if (SelectedObject.tag == "Movable")
            {
               offset = SelectedObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
         }
      }

      if (Input.GetMouseButtonDown(1) && SelectedObject.tag=="Walkable")
      {
         SelectedObject.GetComponent<SoldierUnit>().GoPath(Camera.main.ScreenToWorldPoint(Input.mousePosition));
      }

      if (Input.GetKey(KeyCode.Space))
      {
         GetMouseWorldPosition();
      }
   }


   public void GetMouseWorldPosition()
   {
     Debug.Log( Camera.main.ScreenToWorldPoint(Input.mousePosition) +"isteee");
   }

   public void SelectObject(GameObject selectedObject)
   {
      SelectedObject = selectedObject;
   }
}
