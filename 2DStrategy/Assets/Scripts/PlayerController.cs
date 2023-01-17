using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public GameObject SelectedObject;
   //private Product selectedProduct;
   private Vector3 selectedObjLastPos;
   private Vector3 selectedObjLastStartPos;
   private Building selectedBuilding;
   private Vector3 offset;
   private Camera _camera;
   private BuildingSystem _buildingSystem;

   private void Start()
   {
      _camera = Camera.main;
      _buildingSystem = BuildingSystem.Instance;
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

      if (Input.GetMouseButton(0) &&SelectedObject && SelectedObject.CompareTag("Movable"))
      {
         Vector3 position = offset + GetMouseWorldPosition();
         SelectedObject.transform.position=_buildingSystem.SnapCoordinate(position);
         
         _buildingSystem.RemoveArea(selectedObjLastStartPos,selectedBuilding.productData.ProductSize);
         
         if(_buildingSystem.CanBePlace(selectedBuilding)) {selectedBuilding.ChangeColor(Color.white); }
         else { selectedBuilding.ChangeColor(Color.red); }
      }

      if (Input.GetMouseButtonUp(0) && SelectedObject && SelectedObject.CompareTag("Movable"))
      {
         if(_buildingSystem.CanBePlace(selectedBuilding))
         {
            _buildingSystem.TakeArea(selectedBuilding.startCellPos.position, selectedBuilding.productData.ProductSize);
         }
         else
         {
            SelectedObject.transform.position = selectedObjLastPos;
            _buildingSystem.TakeArea(selectedBuilding.startCellPos.position, selectedBuilding.productData.ProductSize);
         }
         selectedBuilding.ChangeColor(Color.white);
         UnSelectObject();
      }
      
      if (Input.GetMouseButtonDown(1) && SelectedObject&&SelectedObject.CompareTag("Walkable"))
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
     // selectedProduct = null;
      selectedObjLastPos = Vector3.zero;
      selectedObjLastStartPos = Vector3.zero;
   }
   
   public void SelectObject(GameObject selectedObject)
   {
      SelectedObject = selectedObject;
      selectedObjLastPos = SelectedObject.transform.position;
     // selectedProduct = SelectedObject.GetComponent<Product>();
     
      if (SelectedObject.CompareTag("Movable"))
      {
         selectedBuilding = selectedObject.GetComponent<Building>();
         EventManager.SelectedObjectChanged(selectedBuilding.productData);
         selectedObjLastStartPos = selectedBuilding.startCellPos.position;
         offset = SelectedObject.transform.position - GetMouseWorldPosition();
      }
      else if (selectedObject.CompareTag("Walkable"))
      {
         EventManager.SelectedObjectChanged(selectedObject.GetComponent<Product>().productData);
      }
   }
}
