using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : Singleton<BuildingSystem>
{
   public GridLayout GridLayout;
   private Grid grid;
   [SerializeField]private Tilemap tilemap; 
   public Tilemap Tilemap
   {
      get { return tilemap; }
   }


   private void Awake()
   {
      grid = GridLayout.gameObject.GetComponent<Grid>();
   }

   public Vector3 SnapCoordinate(Vector3 position)
   {
      Vector3Int cellPos = GridLayout.WorldToCell(position);
      position = grid.GetCellCenterWorld(cellPos);
      return position;
   }
}
