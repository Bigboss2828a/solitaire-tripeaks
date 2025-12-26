using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] CardMovement cardMovement;
    private Sprite cardBack;
    private Sprite cardFront;
    [SerializeField] Image cardImage;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    public void Init(Sprite back,Sprite front)
    {
        cardBack =  back;
        cardFront = front;
        cardMovement.Init();
    }
    private IEnumerator _CardReveal()
    {
        SoundManager.Instance.PlaySound("Card3");
        cardMovement.CardRot90(rotateSpeed);
        yield return new WaitForSeconds(rotateSpeed);
        CardShowFace();
        cardMovement.CardRot0(rotateSpeed);
    }
    private IEnumerator _CardUnReveal()
    {
        SoundManager.Instance.PlaySound("Card3");
        cardMovement.CardRot90(rotateSpeed);
        yield return new WaitForSeconds(rotateSpeed);
        CardShowBack();

        cardMovement.CardRot0(rotateSpeed);


    }
    public void CardShowFace()
    {
        cardImage.sprite = cardFront;
    }
    public void CardShowBack()
    {
        cardImage.sprite = cardBack;
    }

    public void Reveal()
    {
        StartCoroutine(_CardReveal());
    }
    public void UnReveal()
    {
        StartCoroutine (_CardUnReveal());
    }
    public void CollectBank()
    {
        cardMovement.CardMove(moveSpeed);
        Reveal();
    }
    public void CollectTable()
    {
        int rnd = Random.Range(0, 10);
        if (rnd > 4)
        {
            SoundManager.Instance.PlaySound("Card");

            cardMovement.CardRot360Side(moveSpeed);
            cardMovement.CardMove(moveSpeed);
        }
        else
        {
            SoundManager.Instance.PlaySound("Card2");

            cardMovement.CardMove(moveSpeed*1.4f,Ease.InOutBack);
        }
    }
    public void PutBackTable()
    {
        int rnd = Random.Range(0, 10);
        if (rnd > 4)
        {
            SoundManager.Instance.PlaySound("Card");

            cardMovement.CardRot360Side(moveSpeed);
            cardMovement.CardUnMove(moveSpeed);
        }
        else
        {
            SoundManager.Instance.PlaySound("Card2");

            cardMovement.CardUnMove(moveSpeed * 1.4f, Ease.InOutBack);
        }
    }
    public void PutBackBank()
    {
        cardMovement.CardUnMove(moveSpeed);
        UnReveal();
    }

}
