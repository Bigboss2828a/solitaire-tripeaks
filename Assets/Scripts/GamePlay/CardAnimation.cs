using System.Collections;
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
    }
    private IEnumerator _CardReveal()
    {
        cardMovement.CardRot90(rotateSpeed);
        yield return new WaitForSeconds(rotateSpeed);
        CardShowFace();
        cardMovement.CardRot0(rotateSpeed);
    }
    private IEnumerator _CardUnReveal()
    {
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
    public void Collect()
    {
        cardMovement.CardMove();
    }
    public void PutBack()
    {
        cardMovement.CardUnMove();
    }
}
