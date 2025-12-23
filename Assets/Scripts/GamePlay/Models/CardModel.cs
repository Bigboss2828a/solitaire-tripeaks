using UnityEngine;

public enum CardSuit
{
    Spade,
    Club,
    Heart,
    Diamond
}

[System.Serializable]
public class CardModel
{
    public int cardRank;
    public CardSuit cardSuit;
    public Sprite Sprite;
}
