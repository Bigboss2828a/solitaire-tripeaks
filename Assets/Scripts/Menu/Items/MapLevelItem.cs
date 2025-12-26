using UnityEngine;
using UnityEngine.UI;


public class MapLevelItem : MonoBehaviour
{
    [SerializeField] MapLevelModel levelData;
    [SerializeField] Button btnJoinLevel;
    private void Start()
    {
        btnJoinLevel.onClick.AddListener(OnJoinClicled);
    }
    public MapLevelModel LevelData()
    {
        return levelData;
    }
    void OnJoinClicled()
    {
        ActionManager.OnLevelClicked.Invoke(this);
    }

}
