using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class CardLogic
{


    static bool ChaindCards(int firstNumber, int secondNumber)
    {
        if (firstNumber - secondNumber == 1 || firstNumber - secondNumber == -1)
        {
            return true;
        }
        return false;
    }
    public static bool Match(int firstNumber,int secondNumber)
    {
            if (ChaindCards(firstNumber, secondNumber))
            {

                return true;
            }

        if (firstNumber == 13)
        {
            if (secondNumber == 1)
            {
                return true;
            }
            return false;
        }
        if (firstNumber == 1)
        {
            if (secondNumber == 13)
            {
                return true;
            }
            return false;
        }

        return false;
    }
}

public class GameManager : MonoBehaviour, ICustomStart
{
    public static GameManager instance;
    [Header("Managers")]
    [Space(5)]
    public CardsHolder cardsHolder;
   [SerializeField] CollectedCardManager collectedCardManager;
   [SerializeField] TableCardManager tableCardManager;
   [SerializeField] BankManager bankManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] Transform _collectPos; public Transform collectPos() { return _collectPos; }
    public List<CardModel> poolCards;
    public List<GameObject> Levels;
    [Space(10)]
    [Header("UI")]
    [SerializeField] GamePanelManger winPanel;
    [SerializeField] GamePanelManger exitPanel;
    [SerializeField] Button btnGameExit;
    private int UndoCost = 10;
    


    public void CustomStart()
    {
        winPanel.Init();
        exitPanel.Init();

        btnGameExit.onClick.AddListener(OnExitButtonClicked);
        cardsHolder.Init();
        ActionManager.OnCardCollected += CollectCard;
        StartCoroutine(RunGame());
    }
    IEnumerator RunGame()
    {
        ActionManager.OnCoinChange.Invoke(PlayerDataManager.playerData.playerCoin);
        tableCardManager.TableSetup();
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
        ActionManager.OnCardCollected -= CollectCard;
    }

    public void CollectCard(CardItem cardItem)
    {

        if (cardItem.state() != CardState.OnBank)
        {
            if (!CardLogic.Match(cardItem.cardModel.cardRank,collectedCardManager.ReturnLastCard().cardModel.cardRank))
            {
                return;
            }
            int cardValue = cardItem.UseCard();
            if (cardValue>0)
            {
                PlayerDataManager.Increase(cardValue);
                ActionManager.OnCoinChange.Invoke(PlayerDataManager.GetData().playerCoin);
            }
            cardItem.ValidCard();
            tableCardManager.RemoveCard(cardItem);
            if (TableOut())
            {
                SoundManager.Instance.PlaySound("Win");

                winPanel.OpenPanel();
            }
        }
        else
        {
            cardItem.ValidCard();

            bankManager.RemoveCard(cardItem);
        }
        collectedCardManager.AddCard(cardItem);

        if (collectedCardManager.Count() > 1)
        {
            uiManager.ActiveUndo(true);
        }
        else
        {
            uiManager.ActiveUndo(false);
        }

    }
    private bool TableOut()
    {
       int count = tableCardManager.GetCardCount();
        if (count == 0) 
        {
            return true;
        }
        return false;
    }
    public void UnCollectCard()
    {
        if (!PlayerDataManager.DecreaseCoin(UndoCost))
        {
            return;
        }
            UndoCost = UndoCost * 2;
            ActionManager.OnCoinChange.Invoke(PlayerDataManager.playerData.playerCoin);

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
        if (collectedCardManager.Count() > 1)
        {
            uiManager.ActiveUndo(true);
        }
        else
        {
            uiManager.ActiveUndo(false);
        }
    }
    private void OnExitButtonClicked()
    {
        exitPanel.OpenPanel();
    }
    public void ExitGame()
    {
        StartCoroutine(ExitProcess());
    }
    private IEnumerator ExitProcess()
    {
        yield return new WaitForSeconds(0.5f);
        CloudManager.Instance.OpenClouds();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(0);
    }
}
