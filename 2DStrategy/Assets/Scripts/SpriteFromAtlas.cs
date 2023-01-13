using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class SpriteFromAtlas : MonoBehaviour
{
   [SerializeField]private string spriteName;
   [SerializeField]private SpriteAtlas atlas;

   private void Start()
   {
      GetComponent<SpriteRenderer>().sprite = atlas.GetSprite(spriteName);
   }
}
