using System.Collections.Generic;
using UnityEngine;

public class CollectedCardManager : MonoBehaviour, ICustomStart, ICardList
{
    [SerializeField] List<CardItem> cards;

    public void CustomStart()
    {
      
    }
    public void AddCard(CardItem item)
    {
        cards.Add(item);
    }
    public void RemoveCard(CardItem item)
    {
        cards.Remove(item);
    }
    public void RemoveLast()
    {
        cards.RemoveAt(cards.Count - 1);
    }
    public CardItem ReturnLastCard()
    {
        return cards[cards.Count - 1];
    }
    public int Count()
    {
        return cards.Count;
    }
}
