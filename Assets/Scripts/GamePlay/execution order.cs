using UnityEngine;
public interface ICustomStart
{
   public void CustomStart();
}
public class ExecutionOrder : MonoBehaviour
{
    public MonoBehaviour gameManager;
    public MonoBehaviour actionManager;
    public MonoBehaviour tableCardsManager;
    public MonoBehaviour collectedCardsManager;
    private void Awake()
    {
        CallCustomStart(actionManager);
        CallCustomStart(gameManager);
        CallCustomStart(tableCardsManager);
        CallCustomStart(collectedCardsManager);
    }

    private void CallCustomStart(MonoBehaviour mb)
    {
        if (mb is ICustomStart customStart)
        {
            customStart.CustomStart();
        }
    }
}
