using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
   [SerializeField] private int second;
   [SerializeField] private int minute;

   [SerializeField] private TextMeshProUGUI timeText;


   public void WaitTime()
   {
      int sec = 60 * minute + second;
      StartCoroutine(CO_Wait(sec));
   }

   IEnumerator CO_Wait(int secondtime)
   {
      int min= (int) secondtime / 60;
      int sec =  secondtime % 60;
      
      timeText.gameObject.SetActive(true);
      timeText.text = min.ToString("00") + ":" + sec.ToString("00");

      for(int i=0;i<secondtime;i++)
      {
        
         if (sec > 0)
         {
            sec--;
            yield return new WaitForSecondsRealtime(1);
         }
         else if (min > 0 && sec <= 0)
         {
            min--;
            sec = 59;
            yield return new WaitForSecondsRealtime(1);
         }
        
         timeText.text = min.ToString("00") + ":" + sec.ToString("00");
      }

      timeText.gameObject.SetActive(false); 
      //ButtonActive
      Debug.Log("buttonactive");
            
   }
}
