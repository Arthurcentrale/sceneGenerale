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
    Player player;
    public Vector3 positiondevantmairie;
    public Vector3 positiondansmairie;
    bool inMairie;
    // Start is called before the first frame update
    void Start()
    {
        inMairie = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //positionactuelle = player.transform.position;
    }


    public void  Changermentscene()
    {
        if (!inMairie)
        {
            StartCoroutine(FadeToBlackCoroutine());
            player.transform.position = positiondansmairie;
            inMairie =!inMairie;
        }
        else
        {
            StartCoroutine(FadeToBlackCoroutine());
            player.transform.position = positiondevantmairie;
            inMairie = !inMairie;
        }
    }
    public void EnterMairie()
    {
        
        StartCoroutine(FadeToBlackCoroutine());
        player.transform.position = positiondansmairie;
        //loadingOperation = SceneManager.LoadSceneAsync("Mer i");
        //SceneManager.LoadScene("Mer i");
    }

    public void EnterIsland()
    {
        StartCoroutine(FadeToBlackCoroutine());
        player.transform.position = positiondevantmairie;
        //loadingOperation = SceneManager.LoadSceneAsync("Island");

        //SceneManager.LoadScene("Island");
    }
    private IEnumerator FadeToBlackCoroutine()
    {
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
        loadingScreen.SetActive(false);

    }
    /*private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        player.transform.position = positiondevantmairie;
    }*/
}
