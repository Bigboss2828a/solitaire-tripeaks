using System.Collections.Generic;
using UnityEngine;

public class BankManager : MonoBehaviour, ICustomStart, ICardList
{
    [SerializeField] Transform spwanPos;
    [SerializeField] List<CardItem> cards;
    [SerializeField] List<CardModel> models;
    [SerializeField] List<GameObject> fakeCards;

    public void CustomStart()
    {
        //return;
        SetupBankCards();
    }
    void SetupBankCards()
    {
        models = GameManager.instance.cardsHolder.TakeRestCards();
        for (int i = 0; i < models.Count; i++)
        {
            CardItem item = CreateCard();
            item.FeedData(models[i], CardState.OnBank);
         
            cards.Add(item);
        }
        GameManager.instance.CollectCard(ReturnLastCard());
    }
    CardItem CreateCard()
    {
        GameObject card = Instantiate(GameManager.instance.cardsHolder.cardPrefab, spwanPos);
        card.transform.localPosition = Vector3.zero;
        return card.GetComponent<CardItem>();
    }
    public void AddCard(CardItem item)
    {
        cards.Add(item);
        ChangeFakeCards();
    }
    public void RemoveCard(CardItem item)
    {
        cards.Remove(item);
        ChangeFakeCards();
    }
    public void RemoveLast()
    {
        cards.RemoveAt(cards.Count - 1);
        ChangeFakeCards();
    }
    public CardItem ReturnLastCard()
    {
        return cards[cards.Count - 1];
    }
    public int Count()
    {
        return cards.Count;
    }
    void ChangeFakeCards()
    {
        if (cards.Count ==4)
        {
            fakeCards[0].SetActive(true);
            fakeCards[1].SetActive(true);
            fakeCards[2].SetActive(true);
        }
        if (cards.Count == 3)
        {
            fakeCards[0].SetActive(false);
            fakeCards[1].SetActive(true);
            fakeCards[2].SetActive(true);
        }
        if (cards.Count == 2)
        {
            fakeCards[0].SetActive(false);
            fakeCards[1].SetActive(false);
            fakeCards[2].SetActive(true);
        }
        if (cards.Count == 1)
        {
            fakeCards[0].SetActive(false);
            fakeCards[1].SetActive(false);
            fakeCards[2].SetActive(false);
        }
    }
}
