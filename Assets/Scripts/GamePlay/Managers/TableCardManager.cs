using System.Collections.Generic;
using UnityEngine;

public class TableCardManager : MonoBehaviour, ICustomStart
{

    public List<CardItem> cards;

    public void CustomStart()
    {
        ActionManager.instance.OnAddStack += AddStackToList;

        List<CardModel> cardModels = GameManager.instance.cardsHolder.GetRandomCardsFromPool(cards.Count);
        for (int i = 0; i < cards.Count; i++)
        {

            cards[i].FeedData(cardModels[i], cards[i].state());

        }
    }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        ActionManager.instance.OnAddStack-= AddStackToList;

    }
  
    private void AddStackToList(List<CardItem> stack)
    {
        cards.AddRange(stack);
    }
    public void AddCard(CardItem crd) {
        cards.Add(crd);
    }
    public void RemoveCard(CardItem crd)
    {
        cards.Remove(crd);
    }
    public int GetCardCount()
    {
        return cards.Count; 
    }
}
