using DG.Tweening;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    [SerializeField] Transform startPosition;
    [SerializeField] Transform destinationPosition;
    Vector3 cachedStartPos;
    void Start()
    {
        cachedStartPos = startPosition.position;
        Init();
    }
    public void Init()
    {
        destinationPosition = GameManager.instance.collectPos();
        
    }
    [ContextMenu("Move")]
    public void CardMove(float dur, Ease ease = Ease.Linear)
    {        
        Vector3 desPos = destinationPosition.position;
        gameObject.transform.DOMove(desPos, dur).SetEase(ease);        
    }
    [ContextMenu("ReverseMove")]
    public void CardUnMove(float dur, Ease ease = Ease.Linear)
    {
        gameObject.transform.DOMove(cachedStartPos, dur).SetEase(ease);
    }
    public void CardRot90(float dur)
    {
        transform.DORotate(Vector3.up * 90, dur).SetEase(Ease.Linear);
    }
    public void CardRot0(float dur)
    {
        transform.DORotate(Vector3.zero , dur).SetEase(Ease.Linear);
    }
    public void CardRot360Side(float dur)
    {
        transform.DORotate(Vector3.forward * 360, dur,RotateMode.FastBeyond360).SetEase(Ease.Linear);
    }
}
