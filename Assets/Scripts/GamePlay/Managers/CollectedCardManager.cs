using System.Collections.Generic;
using UnityEngine;

public class CollectedCardManager : MonoBehaviour, ICustomStart
{
    [SerializeField] List<CardItem> Collectedcards;

    public void CustomStart()
    {
      
    }
    public void AddCard(CardItem item)
    {
        Collectedcards.Add(item);
    }
    public void RemoveCard(CardItem item)
    {
        Collectedcards.Remove(item);
    }
    public void RemoveLast()
    {
        Collectedcards.RemoveAt(Collectedcards.Count - 1);
    }
    public CardItem ReturnLastCard()
    {
        return Collectedcards[Collectedcards.Count - 1];
    }
}
