using System.Collections.Generic;
using UnityEngine;

public class TableStack : MonoBehaviour
{
    [SerializeField] bool Ignore;
    private List<CardItem> myCards;
    public void Start()
    {
        
      
    }
    private void OnEnable()
    {
        //ActionManager.instance.callStacks += GiveCardsToTable;
        GiveCardsToTable();
    }
    void OnDisable()
    {
        //ActionManager.instance.callStacks -= GiveCardsToTable;
    }
    void GiveCardsToTable()
    {
        TakeCards();
        ActionManager.instance.OnAddStack.Invoke(myCards);
    }
    void TakeCards()
    {
        if (Ignore) { return; }
        myCards = new List<CardItem>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            CardItem card = child.GetComponent<CardItem>();
            if (card != null)
            {
                myCards.Add(card);
            }
        }
    }
}
