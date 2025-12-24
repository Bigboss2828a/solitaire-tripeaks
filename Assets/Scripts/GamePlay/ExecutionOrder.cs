using UnityEngine;

public class ExecutionOrder : MonoBehaviour
{
    public MonoBehaviour gameManager;
    public MonoBehaviour actionManager;
    public MonoBehaviour tableCardsManager;
    public MonoBehaviour collectedCardsManager;
    private void Start()
    {
      
        CallCustomStart(gameManager);
        CallCustomStart(collectedCardsManager);
        CallCustomStart(tableCardsManager);
    }
    private void Awake()
    {
        CallCustomStart(actionManager);
    }
    private void CallCustomStart(MonoBehaviour mb)
    {
        if (mb is ICustomStart customStart)
        {
            customStart.CustomStart();
        }
    }
}
