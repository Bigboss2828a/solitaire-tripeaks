using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionManager 
{
   // public static ActionManager instance;
    public static Action<CardItem> OnCardCollected;
    public static Action callStacks;
    public static Action<List<CardItem>> OnAddStack;
    public static Action<CardItem> OnAddSIngleCard;

    public static Action<MapLevelItem> OnLevelClicked;
    public static Action<int> OnCoinChange;
    /*    public void CustomStart()
        {
            instance = this;
        }*/
}
