using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject markerPoint;
    private void Start()
    {
        ActionManagerMenu.Instance.OnLevelClicked += LevelClicked;
    }

    private void OnDisable()
    {
        ActionManagerMenu.Instance.OnLevelClicked -= LevelClicked;

    }
    public void LevelClicked(MapLevelItem mli)
    {
        markerPoint.transform.position = mli.transform.position;
    }
    
}
