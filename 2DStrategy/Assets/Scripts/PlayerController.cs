using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public GameObject SelectedObject;
   private Product selectedProduct;
   private Transform selectedObjLastPos;
   private Vector3 offset;
   private Camera _camera;

   private void Start()
   {
      _camera = Camera.main;
     // EventManager.onProductPanelSpawnedObj += SelectedObjChangedByProductPanel;
   }

   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         Vector2 raycastPos=GetMouseWorldPosition();
         RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

         if (hit.collider != null)
         {
            SelectObject(hit.transform.gameObject);

         }
         else
         {
            UnSelectObject();
         }
      }

      if (Input.GetMouseButton(0) &&SelectedObject &&SelectedObject.tag=="Movable")
      {
         Vector3 position = offset + GetMouseWorldPosition();
         SelectedObject.transform.position=BuildingSystem.Instance.SnapCoordinate(position);
         
         if(BuildingSystem.Instance.CanBePlace(selectedProduct)) {selectedProduct.ChangeColor(Color.white); }
         else { selectedProduct.ChangeColor(Color.red); }
      }

      if (Input.GetMouseButtonUp(0) && SelectedObject && SelectedObject.tag == "Movable")
      {
         if(BuildingSystem.Instance.CanBePlace(selectedProduct))
         {
            BuildingSystem.Instance.TakeArea(selectedProduct.startCellPos.position, selectedProduct.productData.ProductSize);
         }
         else
         {
            SelectedObject.transform.position = selectedObjLastPos.position;
         }

         selectedProduct.ChangeColor(Color.white);
         UnSelectObject();
      }
     

      if (Input.GetMouseButtonDown(1) && SelectedObject&&SelectedObject.tag=="Walkable")
      {
         SelectedObject.GetComponent<SoldierUnit>().GoPath(GetMouseWorldPosition());
      }

   }


   public Vector3 GetMouseWorldPosition()
   {
     return  _camera.ScreenToWorldPoint(Input.mousePosition);
   }

   /*public void SelectedObjChangedByProductPanel(GameObject gameObject)
   {
      /*SelectedObject = gameObject;
      selectedProduct = SelectedObject.GetComponent<Product>();
      SelectedObject.tag = "Movable";
      SelectedObject.transform.position = new Vector3(GetMouseWorldPosition().x,GetMouseWorldPosition().y,0);
      SelectedObject.gameObject.SetActive(true);
      Debug.Log("odododo");
      if (SelectedObject.tag == "Movable")
      {
         offset = SelectedObject.transform.position - GetMouseWorldPosition();
      }#1#
   }*/

   public void UnSelectObject()
   {
      SelectedObject = null;
      selectedProduct = null;
      selectedObjLastPos = null;
   }
   public void SelectObject(GameObject selectedObject)
   {
      SelectedObject = selectedObject;
      selectedObjLastPos = SelectedObject.transform;
      selectedProduct = SelectedObject.GetComponent<Product>();
      EventManager.SelectedObjectChanged(selectedProduct.productData);
      if (SelectedObject.tag == "Movable")
      {
         offset = SelectedObject.transform.position - GetMouseWorldPosition();
      }
   }
}
