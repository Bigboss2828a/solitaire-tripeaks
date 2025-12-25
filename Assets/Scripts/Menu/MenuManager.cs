using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Refreces")]
    [SerializeField] MapManager mapManager;
    [SerializeField] GamePanelManger gameLoader;
    [Space(10)]
    [Header("UI")]
   
    [SerializeField] Button btnLoadLevel;

    private MapLevelModel currentMapLevelModel;

    private void Start()
    {
        gameLoader.Init();
      
        ActionManagerMenu.Instance.OnLevelClicked += LevelClicked;
        btnLoadLevel.onClick.AddListener(LoadTheGameScene);
        CloudManager.Instance.CloseClouds();
    }
    private void OnDisable()
    {
        ActionManagerMenu.Instance.OnLevelClicked -= LevelClicked;

    }
    public void LevelClicked(MapLevelItem mli)
    {
        currentMapLevelModel = mli.LevelData();
        StartCoroutine(OpenLevelOpenerPanel(mli));
    }
    IEnumerator OpenLevelOpenerPanel(MapLevelItem mli)
    {
        yield return new WaitForSeconds(0.7f);
        gameLoader.OpenPanel();
    }


    public void LoadTheGameScene(/*MapLevelModel level*/)
    {
        PlayerPrefs.SetInt("Section", currentMapLevelModel.Section);
        PlayerPrefs.SetInt("Level", currentMapLevelModel.Level);
        StartCoroutine(LoadingProcess());
    }
   private IEnumerator LoadingProcess()
    {
        yield return new WaitForSeconds(0.3f);
        CloudManager.Instance.OpenClouds();

        yield return new WaitForSeconds(3.3f);
        SceneManager.LoadScene(1);

    }
}
