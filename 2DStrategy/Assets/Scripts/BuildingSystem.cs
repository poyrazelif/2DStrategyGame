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
   [SerializeField] private TileBase colorTile;
   public Transform newProductSpawnPosition;
   public LineRenderer line;
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

   public bool CanBePlace(Product product)
   {
      BoundsInt area = new BoundsInt();

      area.size = product.productData.ProductSize;
      area.position =GridLayout.WorldToCell(product.startCellPos.position) ;

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
      tilemap.BoxFill(startInt,colorTile,startInt.x,startInt.y,startInt.x+size.x,startInt.y+size.y);
   }
   
}
