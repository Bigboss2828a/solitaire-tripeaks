using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] Button btnUndo;
    [Space(10)]
    [Header("Level")]
    [SerializeField] List<Sprite> Backgrounds;
    [SerializeField] Image backGround;
    private void Start()
    {
        btnUndo.onClick.AddListener(GameManager.instance.UnCollectCard);
    }
    public void SetBackground(int section)
    {
        backGround.sprite = Backgrounds[section];
    }
    public void ActiveUndo(bool active)
    {
        btnUndo.gameObject.SetActive(active);
    }
}
