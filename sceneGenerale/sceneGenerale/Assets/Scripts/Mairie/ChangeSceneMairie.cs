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
    static Vector3 positionactuelle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //positionactuelle = player.transform.position;
        if(positionactuelle != new Vector3(0, 0, 0))
        {
            player.transform.position = positionactuelle;
        }
        positionactuelle = new Vector3(0, 0, 0);
    }

    public void EnterMairie()
    {
        positiondevantmairie = player.transform.position;
        StartCoroutine(FadeToBlackCoroutine());
        loadingOperation = SceneManager.LoadSceneAsync("Mer i");
        //SceneManager.LoadScene("Mer i");
    }

    public void EnterIsland()
    {
        positionactuelle = positiondevantmairie;
        StartCoroutine(FadeToBlackCoroutine());
        loadingOperation = SceneManager.LoadSceneAsync("Island");

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

    }
    /*private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        player.transform.position = positiondevantmairie;
    }*/
}
