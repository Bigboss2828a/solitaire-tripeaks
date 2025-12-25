using UnityEngine;

public interface ICardList 
{
    public void AddCard(CardItem item);
    public void RemoveCard(CardItem item);
    public void RemoveLast();
    public CardItem ReturnLastCard();
    public int Count();
}
