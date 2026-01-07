
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum CardState
{
    Collected,
    OnTable,
    OnTableFaceUp,
    OnBank
}

public class CardItem : MonoBehaviour
{
    public CardModel cardModel;
    public bool defaultData;
    [SerializeField] CardAnimation cardAnimation;
    [SerializeField] Button cardButton;
    [SerializeField] Sprite cardBack;
    [SerializeField] List<CardItem> CoveredByThem;
    [SerializeField] List<CardItem> CoverThem;
    private CardState cardStatePrevious;
    private int originalSiblingIndex;
    private Transform originalParent;
    [SerializeField] CardState cardState ; public CardState state() { return cardState; }
    public void FeedData(CardModel card,CardState state)
    {
        cardModel = card;
        cardAnimation.Init(cardBack, cardModel.Sprite);
        cardState = state;
        Init();
    }
    private void Start()
    {
        StartCoroutine(Setup());
    }
    IEnumerator Setup()
    {
        yield return new WaitForEndOfFrame();
        if (defaultData)
        {
            Init();
            if (cardState != CardState.OnBank)
            {

            ActionManager.instance.OnAddSIngleCard.Invoke(this);
            }
       
        }

    }
    private void Init()
    {
        if (CoveredByThem.Count == 0 && !defaultData)
        {
            cardState = CardState.OnTableFaceUp;
        }
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardClick);
        cardAnimation.Init(cardBack, cardModel.Sprite);
        cardStatePrevious = cardState;
        cardAnimation.CardShowBack();
        switch (cardState)
        {
            case CardState.Collected:
                cardAnimation.CardShowFace();

                break;
            case CardState.OnTable:
                cardAnimation.CardShowBack();
                break;
            case CardState.OnTableFaceUp:
                cardAnimation.Reveal();

                break;
            case CardState.OnBank:
                cardAnimation.CardShowBack();

                break;
        }
        originalSiblingIndex = transform.GetSiblingIndex();
        originalParent = transform.parent;
    }
    void OnCardClick()
    {
        if (cardState == CardState.OnTableFaceUp|| cardState == CardState.OnBank)
        {
        StartCoroutine(DoAction(CardState.Collected));
        }
    }
  private IEnumerator DoAction(CardState newState)
    {
        if (newState == CardState.Collected)
        {
            if (cardState == CardState.OnTableFaceUp)
            {
                cardAnimation.CollectTable();
            }
            if (cardState == CardState.OnBank)
            {
                cardAnimation.CollectBank();
            }
                SetParent();
            ActionManager.instance.OnCardCollected.Invoke(this);
        }
        if (newState== CardState.OnTableFaceUp)
        {
            cardAnimation.Reveal();
        }
        if (newState == CardState.OnTable)
        {
            if (cardState != CardState.OnTable)
            {
                cardAnimation.UnReveal();
            }
        }
        //
        cardStatePrevious = cardState;
        cardState = newState;
        NotifyCardsBehind();
        yield return new WaitForSeconds (0.5f);
    }
    public void UnDo()
    {
        StartCoroutine(UnDoAction());
    }
    private IEnumerator UnDoAction()
    {
        if (cardStatePrevious == CardState.OnTableFaceUp)
        {
            cardAnimation.PutBackTable();
        }
        if (cardStatePrevious == CardState.OnBank)
        {
            cardAnimation.PutBackBank();
        }
        ReParent();
        //
        cardState = cardStatePrevious;
        NotifyCardsBehind();
        yield return new WaitForSeconds (0.5f);

    }
    void NotifyCardsBehind()
    {
        foreach (var item in CoverThem)
        {
            item.FrontCardMoved();
        }
    }
    public void FrontCardMoved()
    {
        bool hasCover = false;
        foreach (var item in CoveredByThem)
        {
            if (item.cardState != CardState.Collected)
                hasCover = true;
        }
        if (!hasCover)
        {
            StartCoroutine(DoAction(CardState.OnTableFaceUp));
        }
        else
        {
            StartCoroutine(DoAction(CardState.OnTable));
        }
    }
    public void SetParent()
    {
             
        transform.parent = GameManager.instance.collectPos();
        transform.SetAsLastSibling();
    }
    public void ReParent()
    {
        transform.parent = originalParent;
        transform.SetSiblingIndex(originalSiblingIndex);
    }

    public Sprite CardBack()
    {
        return cardBack;
    }
    public Sprite CardFront()
    {
        return cardModel.Sprite;
    }
}
