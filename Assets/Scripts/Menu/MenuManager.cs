using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Refreces")]
    [SerializeField] MapManager mapManager;
    [SerializeField] GameLoaderPanelManger gameLoader;
    [Space(10)]
    [Header("UI")]
    [SerializeField] Button btnCloseGameLoader;
    [SerializeField] Button btnLoadLevel;

    private MapLevelModel currentMapLevelModel;

    private void Start()
    {
        gameLoader.Init();
        btnCloseGameLoader.onClick.AddListener(CloseGameLoader);
        ActionManagerMenu.Instance.OnLevelClicked += LevelClicked;
        btnLoadLevel.onClick.AddListener(LoadTheGameScene);
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
    void CloseGameLoader()
    {
        gameLoader.ClosePanel();
    }

    public void LoadTheGameScene(/*MapLevelModel level*/)
    {
        PlayerPrefs.SetInt("Section", currentMapLevelModel.Section);
        PlayerPrefs.SetInt("Level", currentMapLevelModel.Level);
        StartCoroutine(LoadingProccess());
    }
    IEnumerator LoadingProccess()
    {
        yield return new WaitForSeconds(0.3f);
        CloudManager.Instance.OpenClouds();

        yield return new WaitForSeconds(3.3f);
        SceneManager.LoadScene(1);

    }
}
