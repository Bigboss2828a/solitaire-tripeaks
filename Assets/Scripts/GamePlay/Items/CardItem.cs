
using System.Collections;
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
    [SerializeField] CardAnimation cardAnimation;
    [SerializeField] Button cardButton;
    [SerializeField] Sprite cardBack;
    [SerializeField] CardState cardState;
    private CardState cardStatePrevious;

    public void FeedData(CardModel card)
    {
        cardAnimation.Init(cardBack, cardModel.Sprite);
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardClick);
        cardAnimation.Init(cardBack, cardModel.Sprite);
        cardStatePrevious = cardState;
        switch (cardState)
        {
            case CardState.Collected:
                cardAnimation.CardShowFace();

                break;
            case CardState.OnTable:
                cardAnimation.CardShowBack();
                break;
            case CardState.OnTableFaceUp:
                cardAnimation.CardShowFace();

                break;
            case CardState.OnBank:
                cardAnimation.CardShowBack();

                break;
            default:
                break;
        }
    }
    void OnCardClick()
    {
        StartCoroutine(DoAction());
    }
  private IEnumerator DoAction()
    {
        SetParent();
        if (cardState == CardState.OnTableFaceUp)
        {
            cardAnimation.Collect();

        }
        if (cardState == CardState.OnBank)
        {
            cardAnimation.Collect();
            cardAnimation.Reveal();
        }
        yield return new WaitForSeconds (0.5f);
        cardState = CardState.Collected;
    }
    public IEnumerator UnDoAction()
    {
        if (cardStatePrevious == CardState.OnTableFaceUp)
        {
            cardAnimation.PutBack();
        }
        if (cardStatePrevious == CardState.OnBank)
        {
            cardAnimation.PutBack();
            cardAnimation.UnReveal();
        }
        yield return new WaitForSeconds (0.5f);
        cardState = cardStatePrevious;

    }

    public void SetParent()
    {
        
      
        transform.parent = GameManager.instance.collectPos();
        transform.SetAsLastSibling();
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
