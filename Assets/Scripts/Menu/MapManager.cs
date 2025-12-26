using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject markerPoint;
    private void Start()
    {
        ActionManager.OnLevelClicked += LevelClicked;
    }

    private void OnDisable()
    {
        ActionManager.OnLevelClicked -= LevelClicked;

    }
    public void LevelClicked(MapLevelItem mli)
    {
        markerPoint.transform.position = mli.transform.position;
    }
    
}
