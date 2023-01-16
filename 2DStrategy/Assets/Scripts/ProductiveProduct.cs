using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductiveProduct :Product
{
  public bool isPlaced=false;
  [SerializeField] private Transform SpawnPosition;
  private Timer timer;
  private void Start()
  {
    timer = GetComponent<Timer>();
    timer.SetTime(productData.ProduceTimeMin,productData.ProduceTimeSec);
    timer.WaitTime();
  }
  
  //yerleştiğinde timer başlayacak

  public void Spawn()
  {
    foreach (var producedThing in productData.ProducedThings)
    {
      GameObject Obj = ObjectPool.Instance.GetFromPool(producedThing.ProductName);
      Obj.transform.position = BuildingSystem.Instance.SnapCoordinate(SpawnPosition.position);
      Obj.transform.localScale=Vector3.one;
      Obj.SetActive(true);
    }
  }
}
