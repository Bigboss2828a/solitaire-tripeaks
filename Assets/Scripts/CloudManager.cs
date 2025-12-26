using DG.Tweening;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public static CloudManager Instance;
    [SerializeField] Transform rightHidePos;
    [SerializeField] Transform leftHidePos;
    [SerializeField] GameObject rightSide;
    [SerializeField] GameObject leftSide;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void init()
    {
        
    }
    [ContextMenu("Open")]
    public void OpenClouds()
    {
        SoundManager.Instance.PlaySound("Cloud");
        rightSide.SetActive(true);
        leftSide.SetActive(true);
        rightSide.transform.DOLocalMove(Vector3.zero, .9f);
        leftSide.transform.DOLocalMove(Vector3.zero, 0.9f);

    }
    [ContextMenu("Close")]
    public void CloseClouds()
    {
        SoundManager.Instance.PlaySound("Cloud");

        rightSide.transform.DOMove(rightHidePos.position, .9f).OnComplete(() => { rightSide.SetActive(false); });
        leftSide.transform.DOMove(leftHidePos.position, .9f).OnComplete(() => { leftSide.SetActive(false); });
    }
}
