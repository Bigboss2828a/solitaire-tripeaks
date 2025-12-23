using DG.Tweening;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    [SerializeField] Transform startPosition;
    [SerializeField] Transform destinationPosition;
    Vector3 cachedStartPos;
    void Start()
    {
        Init();
    }
    private void Init()
    {
        destinationPosition = GameManager.instance.collectPos();
        cachedStartPos = startPosition.position;
    }
    [ContextMenu("Move")]
    public void CardMove()
    {        
        Vector3 desPos = destinationPosition.position;
        gameObject.transform.DOMove(desPos, 1).SetEase(Ease.Linear);        
    }
    [ContextMenu("ReverseMove")]
    public void CardUnMove()
    {
        gameObject.transform.DOMove(cachedStartPos, 1).SetEase(Ease.Linear);
    }
    public void CardRot90(float dur)
    {
        transform.DORotate(Vector3.up * 90, dur).SetEase(Ease.Linear);
    }
    public void CardRot0(float dur)
    {
        transform.DORotate(Vector3.zero , dur).SetEase(Ease.Linear);
    }
}
