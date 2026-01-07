using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour,ICustomStart
{
    public static ActionManager instance;
    public Action<CardItem> OnCardCollected;
    public Action callStacks;
    public Action<List<CardItem>> OnAddStack;
    public Action<CardItem> OnAddSIngleCard;
    private void Awake()
    {
       
    }
    public void CustomStart()
    {
        instance = this;
    }
}
