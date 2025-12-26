using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Button btnOpenShop;
    [SerializeField] int shopCoin;
    [SerializeField] GamePanelManger shopPanel;
    

    private void Start()
    {
        btnOpenShop.onClick.AddListener(OpenShop);
    }
    void SetCoinShop()
    {
        shopPanel.SetText(shopCoin.ToString());
    }
    void OpenShop()
    {
        shopPanel.OpenPanel();
    }
    public void Purchase()
    {
        SoundManager.Instance.PlaySound("Cloud");

        ActionManager.OnCoinChange(PlayerDataManager.Increase(shopCoin));
    }
}
