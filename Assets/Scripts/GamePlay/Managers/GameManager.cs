using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;


public class GameManager : MonoBehaviour, ICustomStart
{
    public static GameManager instance;
    public CardsHolder cardsHolder;
   [SerializeField] CollectedCardManager collectedCardManager;
   [SerializeField] TableCardManager tableCardManager;
   [SerializeField] BankManager bankManager;
    [SerializeField] Transform _collectPos; public Transform collectPos() { return _collectPos; }
    public List<CardModel> poolCards;
    public void CustomStart()
    {

        ActionManager.instance.OnCardCollected += CollectCard;
    }
    private void Awake()
    {
            instance = this;    
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        ActionManager.instance.OnCardCollected -= CollectCard;
    }
    public void UnDoAction()
    {
        UnCollectCard();
    }
    public void CollectCard(CardItem cardItem)
    {
        
        collectedCardManager.AddCard(cardItem);
        if (cardItem.state() == CardState.OnBank)
        {
            bankManager.RemoveCard(cardItem);
            return;
        }
        tableCardManager.RemoveCard(cardItem);

    }
    public void UnCollectCard()
    {
        CardItem cardItem = collectedCardManager.ReturnLastCard();
        cardItem.UnDo();
        if (cardItem.state() == CardState.OnBank)
        {
            bankManager.AddCard(cardItem);
        }
        else
        {
        tableCardManager.AddCard(cardItem);
        }
        collectedCardManager.RemoveLast();
    }
}
