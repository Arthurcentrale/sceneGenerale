using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChangeSceneMairie : MonoBehaviour
{
    AsyncOperation loadingOperation;
    public Slider slider;
    public GameObject loadingScreen;
    // Start is called before the first frame update
    void Update()
    {
        slider.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    }
    public void EnterMairie()
    {
        StartCoroutine(FadeToBlackCoroutine());
        loadingOperation = SceneManager.LoadSceneAsync("Mer i");
        //SceneManager.LoadScene("Mer i");
    }

    public void EnterIsland()
    {
        StartCoroutine(FadeToBlackCoroutine());
        loadingOperation = SceneManager.LoadSceneAsync("Island");
        //SceneManager.LoadScene("Island");
    }
    private IEnumerator FadeToBlackCoroutine()
    {
        Debug.Log("Fading");
        float fade = 1.0f;
        loadingScreen.SetActive(true);
        do
        {
            Color c = loadingScreen.GetComponentInChildren<Image>().color;
            c.a = Mathf.Lerp(0.0f, 1.0f, fade);
            loadingScreen.GetComponentInChildren<Image>().color = c;
            fade -= 1.5f * Time.deltaTime;
            yield return null;
        } while (fade >= 0.0f);
        Debug.Log("Faded");
    }
}
