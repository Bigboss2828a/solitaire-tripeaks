using DG.Tweening;
using UnityEngine;

public class GameLoaderPanelManger : MonoBehaviour
{
    [SerializeField] GameObject gameLoaderPanel;

    public void Init()
    {
        gameLoaderPanel.transform.localScale = Vector3.zero;
        gameLoaderPanel.SetActive(false);
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
