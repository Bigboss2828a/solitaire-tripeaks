using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] Button btnUndo;
    private void Start()
    {
        btnUndo.onClick.AddListener(GameManager.instance.UnCollectCard);
    }
}
