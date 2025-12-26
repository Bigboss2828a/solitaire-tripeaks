using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    [SerializeField] Image fillBar;
    void Start()
    {
        StartCoroutine(FakeLoading());
    }

    IEnumerator FakeLoading()
    {
        fillBar.DOFillAmount(1,18);
        yield return new WaitForSeconds(20);
        SceneManager.LoadScene(1);
    }

}
