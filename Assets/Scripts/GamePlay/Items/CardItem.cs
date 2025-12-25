
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
    [SerializeField] CardState cardState;
    public CardState state() { return cardState; }
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
        if (CoveredByThem.Count == 0 && !defaultData&& cardState !=CardState.OnBank)
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
        if (cardState == CardState.OnTableFaceUp)
        {
            ActionManager.instance.OnCardCollected.Invoke(this);

           // StartCoroutine(DoAction(CardState.Collected));

        }
        if (cardState == CardState.OnBank)
        {
            ActionManager.instance.OnCardCollected.Invoke(this);

           // StartCoroutine(DoAction(CardState.Collected));

        }
    }
    public void ValidCard()
    {
       _DoAction(CardState.Collected);
    }
  private void _DoAction(CardState newState)
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
        Debug.Log("111");
        //    yield return new WaitForSeconds (0.2f);
        if (cardState == CardState.Collected)
        {
          NotifyCardsBehind();
        }
        
    }
    
    public void UnDo()
    {
        //StartCoroutine(UnDoAction());
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
      //  yield return new WaitForSeconds(0.5f);
    }
/*    private IEnumerator UnDoAction()
    {


    }*/
    private int tst;
    void NotifyCardsBehind()
    {
      
        foreach (var item in CoverThem)
        {
            tst++;
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
            _DoAction(CardState.OnTableFaceUp   );
        }
        else
        {
            _DoAction(CardState.OnTable);
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
    public IEnumerator _Scatter()
    {
        transform.position = new Vector3(transform.position.x+Random.Range(-2f,2f), transform.position.y+ Random.Range(-2f, 2f), 0);
        yield return new WaitForSeconds(0.1f);
        cardAnimation.PutBackTable();
    }
    public void Scatter()
    {
        StartCoroutine(_Scatter());
    }

}
