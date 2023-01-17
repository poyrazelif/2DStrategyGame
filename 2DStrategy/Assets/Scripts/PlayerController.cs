using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public GameObject SelectedObject;
   private Product selectedProduct;
   private Vector3 selectedObjLastPos;
   private Vector3 selectedObjLastStartPos;
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
            BuildingSystem.Instance.RemoveArea(selectedObjLastStartPos,selectedProduct.productData.ProductSize);
         }
         else
         {
            SelectedObject.transform.position = selectedObjLastPos;
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

   public void UnSelectObject()
   {
      SelectedObject = null;
      selectedProduct = null;
      selectedObjLastPos = Vector3.zero;
      selectedObjLastStartPos = Vector3.zero;
   }
   public void SelectObject(GameObject selectedObject)
   {
      SelectedObject = selectedObject;
      selectedObjLastPos = SelectedObject.transform.position;
      selectedProduct = SelectedObject.GetComponent<Product>();
      selectedObjLastStartPos = selectedProduct.startCellPos.position;
      EventManager.SelectedObjectChanged(selectedProduct.productData);
      if (SelectedObject.tag == "Movable")
      {
         offset = SelectedObject.transform.position - GetMouseWorldPosition();
      }
   }
}
