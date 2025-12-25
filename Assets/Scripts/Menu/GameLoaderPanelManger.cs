using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePanelManger : MonoBehaviour
{
    [SerializeField] GameObject gameLoaderPanel;
    [SerializeField] Button btnClose;
    
    [SerializeField] Button btnAccept;
    [SerializeField] UnityEvent acceptEvent;

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
        acceptEvent.Invoke();
    }
    public void OpenPanel()
    {
        gameLoaderPanel.SetActive(true);
        gameLoaderPanel.transform.DOScale(Vector3.one, 0.1f);
    }
    public void ClosePanel()
    {
        gameLoaderPanel.SetActive(false);
        gameLoaderPanel.transform.DOScale(Vector3.zero, 0.1f);
    }
}
