using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour, ICustomStart
{
    public static GameManager instance;
    public CardsHolder cardsHolder;
   [SerializeField] CollectedCardManager collectedCardManager;
    [SerializeField] Transform _collectPos; public Transform collectPos() { return _collectPos; }
    public List<CardModel> poolCards;
    public void CustomStart()
    {

    }

    private void Awake()
    {
            instance = this;    
    }
    private void OnEnable()
    {
        ActionManager.instance.OnCardCollected += CollectCard;
    }
    private void OnDisable()
    {
        ActionManager.instance.OnCardCollected -= CollectCard;
    }
    private void Start()
    {
       // cardsHolder.Init();
    }
    public void UnDoAction()
    {
        UnCollectCard();
    }
    public void CollectCard(CardItem cardItem)
    {
        
        collectedCardManager.AddCard(cardItem);
    }
    public void UnCollectCard()
    {
        collectedCardManager.ReturnLastCard() .UnDo();
        collectedCardManager.RemoveLast();
    }
}
