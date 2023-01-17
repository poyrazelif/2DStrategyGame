using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ProductPanelConfigure : MonoBehaviour
{
   [SerializeField]private ProductData productData;

   private Timer timer;
   [SerializeField] private Image image;
   [SerializeField] private TextMeshProUGUI text;

   [SerializeField] private Transform newProductSpawnPosition;
   private void Start()
   { 
       timer = GetComponent<Timer>();
       
       ConfigureProductPanel();
   }

   private void ConfigureProductPanel()
   {
      timer.SetTime(productData.ProductPanelWaitTimeMin,productData.ProductPanelWaitTimeSec);
      
      image.sprite = productData.ProductSprite;
      //image.rectTransform.sizeDelta = productData.ProductSprite.rect.size / 4;


      text.text = productData.ProductName;

   }

   public void SpawnProduct()
   {
       GameObject NewProduct = ObjectPool.Instance.GetFromPool(productData.ProductName);
       EventManager.ProductPanelSpawnedObject(NewProduct);
       NewProduct.transform.position = BuildingSystem.Instance.newProductSpawnPosition.position;
       BuildingSystem.Instance.TakeArea(NewProduct.GetComponent<Product>().startCellPos.position,productData.ProductSize);
       NewProduct.gameObject.SetActive(true);
   }
   
   
}
