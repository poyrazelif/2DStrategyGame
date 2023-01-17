using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

/*[System.Serializable]
public class Produces
{
   public Image ImageOfProduce;
   public string NameOfProduce;
   public int TimeOfProduce;
}*/

[CreateAssetMenu(fileName = "ProductData", menuName = "ScriptableObjects/ProductData", order = 1)]
public class ProductData : ScriptableObject
{
   public string ProductName;

   public Vector3Int ProductSize;

   public Sprite ProductSprite;

   public int ProductPanelWaitTimeSec;
   
   public int ProductPanelWaitTimeMin;
   
   public List<ProductData> ProducedThings;

   public int ProduceTimeMin;
   public int ProduceTimeSec;

}
