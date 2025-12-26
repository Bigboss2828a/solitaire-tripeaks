using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePanelManger : MonoBehaviour
{
    [SerializeField] GameObject gameLoaderPanel;
    [SerializeField] Button btnClose;
    
    [SerializeField] Button btnAccept;
    [SerializeField] UnityEvent acceptEvent;
    [SerializeField] TextMeshProUGUI txtExtra;

    public void Init()
    {
        gameLoaderPanel.transform.localScale = Vector3.zero;
        gameLoaderPanel.SetActive(false);

        if (btnAccept != null)
        {
            btnAccept.onClick.AddListener(OnAcceptClicked);
        }
        if (btnClose != null)
        {
            btnClose.onClick.AddListener(ClosePanel);
        }
    }
    void OnAcceptClicked()
    {
        SoundManager.Instance.PlaySound("Button");

        acceptEvent.Invoke();
    }
    public void SetText(string s)
    {
        txtExtra.text = s;
    }
    public void OpenPanel()
    {
        SoundManager.Instance.PlaySound("Button");
        gameLoaderPanel.SetActive(true);
        gameLoaderPanel.transform.DOScale(Vector3.one, 0.1f);
    }
    public void ClosePanel()
    {
        SoundManager.Instance.PlaySound("Button");
        gameLoaderPanel.transform.DOScale(Vector3.zero, 0.1f);
        gameLoaderPanel.SetActive(false);
    }
}
