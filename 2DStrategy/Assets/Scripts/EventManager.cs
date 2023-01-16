using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    

    public static event Action<ProductData> onSelectedObjChanged;

    public static void SelectedObjectChanged(ProductData productData)
    {
        onSelectedObjChanged?.Invoke(productData);
    }

    
}
