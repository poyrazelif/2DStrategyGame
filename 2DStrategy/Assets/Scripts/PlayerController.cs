using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public GameObject SelectedObject;
   private Vector3 offset;
   private Camera _camera;

   private void Start()
   {
      _camera = Camera.main;
   }

   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         Debug.Log("down");
         Vector2 raycastPos=GetMouseWorldPosition();
         RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);
         
         if (hit.collider!=null)
         {
            SelectObject(hit.transform.gameObject);
           
         }
      }

      if (Input.GetMouseButton(0) &&SelectedObject &&SelectedObject.tag=="Movable")
      {
         Vector3 position = offset + GetMouseWorldPosition();
         SelectedObject.transform.position=BuildingSystem.Instance.SnapCoordinate(position);
      }
     

      if (Input.GetMouseButtonDown(1) && SelectedObject.tag=="Walkable")
      {
         SelectedObject.GetComponent<SoldierUnit>().GoPath(GetMouseWorldPosition());
      }

      if (Input.GetKey(KeyCode.Space))
      {
         GetMouseWorldPosition();
      }
   }


   public Vector3 GetMouseWorldPosition()
   {
     return  _camera.ScreenToWorldPoint(Input.mousePosition);
   }

   public void SelectObject(GameObject selectedObject)
   {
      SelectedObject = selectedObject;
      EventManager.SelectedObjectChanged(selectedObject.GetComponent<Product>().productData);
      if (SelectedObject.tag == "Movable")
      {
         offset = SelectedObject.transform.position - GetMouseWorldPosition();
      }
   }
}
