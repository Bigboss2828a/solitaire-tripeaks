using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Refreces")]
    [SerializeField] MapManager mapManager;
    [SerializeField] GamePanelManger gameLoader;
    [SerializeField] GamePanelManger shopPanel;
    [Space(10)]
    [Header("UI")]
    
    [SerializeField] Button btnLoadLevel;

    private MapLevelModel currentMapLevelModel;
    //private PlayerDataModel currentPlayerDataModel;
    private void Start()
    {

        SetPlayerData();
        SoundManager.Instance.initialize();
        SoundManager.Instance.SetVolumes(0.5f,1);
        SoundManager.Instance.PlayMusic("Music");
       gameLoader.Init();
        shopPanel.Init();
        ActionManager.OnLevelClicked += LevelClicked;
        btnLoadLevel.onClick.AddListener(LoadTheGameScene);
        CloudManager.Instance.CloseClouds();
    }
    void SetPlayerData()
    {
        PlayerDataManager.Initialize();
        PlayerDataManager.LoadData();

        ActionManager.OnCoinChange.Invoke(PlayerDataManager.GetData().playerCoin);
    }
    private void OnDisable()
    {
        ActionManager.OnLevelClicked -= LevelClicked;

    }
    public void LevelClicked(MapLevelItem mli)
    {
        currentMapLevelModel = mli.LevelData();
        StartCoroutine(OpenLevelOpenerPanel(mli));
    }
    IEnumerator OpenLevelOpenerPanel(MapLevelItem mli)
    {
        yield return new WaitForSeconds(0.7f);
        gameLoader.SetText(mli.LevelData().JoinCost.ToString());
        gameLoader.OpenPanel();
    }


    public void LoadTheGameScene(/*MapLevelModel level*/)
    {
        if (PlayerDataManager.DecreaseCoin(currentMapLevelModel.JoinCost))
        {
        PlayerPrefs.SetInt("Section", currentMapLevelModel.Section);
        PlayerPrefs.SetInt("Level", currentMapLevelModel.Level);
        StartCoroutine(LoadingProcess());
        }
    }
   private IEnumerator LoadingProcess()
    {
        yield return new WaitForSeconds(0.3f);
        CloudManager.Instance.OpenClouds();

        yield return new WaitForSeconds(3.3f);
        SceneManager.LoadScene(1);

    }
}
