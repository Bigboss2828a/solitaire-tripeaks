using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(
    fileName = "CardHolder",
    menuName = "Cards/Card"
)]
public class CardsHolder : ScriptableObject
{
    public List<CardModel> spadeCards;
    public List<CardModel> heartCards;

    public List<CardModel> allCards;
    public List<CardModel> poolCards;
    public void Init()
    {
        allCards = MixCards();
        poolCards.AddRange(allCards);
    }
    
    private List<CardModel> MixCards()
    {
        allCards.Clear();
        List<CardModel> allCardsM = new List<CardModel>();
        allCardsM.AddRange(spadeCards);
        allCardsM.AddRange(heartCards);
        for (int i = allCardsM.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (allCardsM[i], allCardsM[j]) = (allCardsM[j], allCardsM[i]);
        }
        return allCardsM;
    }

    public List<CardModel> GetRandomCardsFromPool(int count)
    {
        if (count > poolCards.Count)
        {
            int req = count- poolCards.Count;
            GetExtra(req);
        }
        List<CardModel> result = new List<CardModel>();

        count = Mathf.Min(count, poolCards.Count);

        for (int i = 0; i < count; i++)
        {
            int last = poolCards.Count - 1;
            result.Add(poolCards[last]);
            poolCards.RemoveAt(last);
        }

        return result;
    }
    void GetExtra(int take)
    {
        allCards = MixCards();

        take = Mathf.Min(take, allCards.Count);

        for (int i = 0; i < take; i++)
        {
            poolCards.Add(allCards[i]);
        }
    }
}
