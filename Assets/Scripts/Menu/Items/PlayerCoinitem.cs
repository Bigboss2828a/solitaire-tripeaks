using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerCoinitem : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI playerCoin;

    private void OnEnable()
    {
        ActionManager.OnCoinChange += ValueChange;
    }
    private void OnDisable()
    {
        ActionManager.OnCoinChange -= ValueChange;

    }
    public void ValueChange(int value)
    {
        playerCoin.transform.DOKill();
        playerCoin.transform.DOShakeScale(0.1f,0.5f);
        playerCoin.text = value.ToString(); 
    }

}
