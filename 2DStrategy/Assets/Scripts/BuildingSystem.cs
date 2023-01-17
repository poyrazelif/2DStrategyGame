using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : Singleton<BuildingSystem>
{ 
   [SerializeField] private Tilemap tilemap;
   [SerializeField] private TileBase colorTile;
   private Grid grid;
     
   public GridLayout GridLayout;
   public Transform newProductSpawnPosition;
   public LineRenderer line;
   public Tilemap Tilemap { get { return tilemap; } }

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

   public TileBase[] GetTileBlock(Tilemap tilemap, BoundsInt area)
   {
      TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
      int count = 0;

      foreach (var p in area.allPositionsWithin)
      {
         Vector3Int pos = new Vector3Int(p.x, p.y, 0);
         array[count] = tilemap.GetTile(pos);
         count++;
      }
      return array; 
   }

   public bool CanBePlace(Building building)
   {
      BoundsInt area = new BoundsInt();

      area.size = building.productData.ProductSize;
      area.position =GridLayout.WorldToCell(building.startCellPos.position) ;

      TileBase[] baseArray = GetTileBlock(tilemap, area);

      foreach (var b in baseArray)
      {
         if (b == colorTile)
         {
            return false;
         }
      }
      return true;
   }

   public void TakeArea(Vector3 start, Vector3Int size)
   {
      Vector3Int startInt = GridLayout.WorldToCell(start);
      tilemap.BoxFill(startInt,colorTile,startInt.x,startInt.y,startInt.x+size.x-1,startInt.y+size.y-1);
   }

   public void RemoveArea(Vector3 start,Vector3Int size)
   {
      BoundsInt area = new BoundsInt();
      area.size = size;
      area.position =GridLayout.WorldToCell(start) ;

      foreach (var t in area.allPositionsWithin)
      {
         tilemap.SetTile(t,null);
      }
   }
   
}
