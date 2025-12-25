using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;


public class GameManager : MonoBehaviour, ICustomStart
{
    public static GameManager instance;
    [Header("Managers")]
    [Space(10)]
    public CardsHolder cardsHolder;
   [SerializeField] CollectedCardManager collectedCardManager;
   [SerializeField] TableCardManager tableCardManager;
   [SerializeField] BankManager bankManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] Transform _collectPos; public Transform collectPos() { return _collectPos; }
    public List<CardModel> poolCards;
    public List<GameObject> Levels;


    public void CustomStart()
    {
        cardsHolder.Init();
        ActionManager.instance.OnCardCollected += CollectCard;
        StartCoroutine(RunGame());
    }
    IEnumerator RunGame()
    {
        int section = PlayerPrefs.GetInt("Section");
        int level = PlayerPrefs.GetInt("Level");
        yield return null;
        Levels[level].SetActive(true);
        uiManager.SetBackground(section);
        CloudManager.Instance.CloseClouds();
        yield return null;
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
