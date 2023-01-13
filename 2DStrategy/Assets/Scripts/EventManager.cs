using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnBombSelected();

    public static event OnBombSelected BombSelected;

    public delegate void OnPrizeSelected();

    public static event OnPrizeSelected PrizeSelected;

    public delegate void OnPassedNextLevel();

    public static event OnPassedNextLevel PassedNextLevel;

    public static void NextLevel()
    {
        PassedNextLevel?.Invoke();
    }
    
    public static void SelectedBomb()
    {
        BombSelected?.Invoke();
    }

    public static void SelectedPrize()
    {
        PrizeSelected?.Invoke();
    }
    
}
