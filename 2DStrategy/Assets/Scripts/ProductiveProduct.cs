using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductiveProduct :Product
{
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
      Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y, SpawnPosition.position.z);
      Obj.transform.localScale=Vector3.one;
      Obj.SetActive(true);
    }
  }
}
