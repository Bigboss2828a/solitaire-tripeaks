using System.Collections;
using UnityEngine;

public class ExecutionOrder : MonoBehaviour
{
    public MonoBehaviour gameManager;
    public MonoBehaviour actionManager;
    public MonoBehaviour tableCardsManager;
    public MonoBehaviour collectedCardsManager;
    public MonoBehaviour bankCardManager;
    private void Start()
    {

        StartCoroutine(RunScripts());

    }
    private void Awake()
    {
        CallCustomStart(actionManager);
    }
    IEnumerator RunScripts()
    {
        CallCustomStart(gameManager);
        CallCustomStart(collectedCardsManager);       
        CallCustomStart(tableCardsManager);
        yield return new WaitForSeconds (0.1f);
        CallCustomStart(bankCardManager);
    }
    private void CallCustomStart(MonoBehaviour mb)
    {
        if (mb is ICustomStart customStart)
        {
            customStart.CustomStart();
        }
    }
}
