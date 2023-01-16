using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
   private int second;
    private int minute;

   [SerializeField] private TextMeshProUGUI timeText;
   [SerializeField] private TextMeshPro timeTextInGame;

   [Serializable]public class onTimeFinished: UnityEvent { }
   public onTimeFinished TimeFinished;
   
   [Serializable]public class onTimeStarted: UnityEvent { }
   public onTimeFinished TimeStarted;

   public void SetTime(int min, int sec)
   {
      minute = min;
      second = sec;
   }
   public void WaitTime()
   {
      int sec = 60 * minute + second;
      StartCoroutine(CO_Wait(sec));
   }

   IEnumerator CO_Wait(int secondtime)
   {
      if (secondtime <= 0)
      {
         yield break;
      }

      TimeStarted.Invoke();
      
      int min= (int) secondtime / 60;
      int sec =  secondtime % 60;
      if (timeText != null)
      {
         timeText.gameObject.SetActive(true);
         timeText.text = min.ToString("00") + ":" + sec.ToString("00");
      }

      if (timeTextInGame != null)
      {
         timeTextInGame.gameObject.SetActive(true);
         timeTextInGame.text = min.ToString("00") + ":" + sec.ToString("00");
      }
      
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
        
         if (timeText != null)
            timeText.text = min.ToString("00") + ":" + sec.ToString("00");
         
         if(timeTextInGame!=null)
            timeTextInGame.text = min.ToString("00") + ":" + sec.ToString("00");
      }

      if (timeText != null)
         timeText.gameObject.SetActive(false); 
      
      if (timeTextInGame != null)
         timeTextInGame.gameObject.SetActive(false); 
      //ButtonActive
      
      TimeFinished.Invoke();
      /*button.interactable = true;
      Debug.Log("buttonactive");*/
            
   }
}
