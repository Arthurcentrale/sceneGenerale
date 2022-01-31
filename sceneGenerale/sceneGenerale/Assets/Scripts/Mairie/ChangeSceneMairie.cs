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
    Mairie mairie;
    // Start is called before the first frame update
    void Start()
    {
        inMairie = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mairie = GameObject.Find("Camera").GetComponent<Mairie>();
        //positionactuelle = player.transform.position;
    }


    public void  Changermentscene()
    {
        if (!inMairie)
        {
            StartCoroutine(FadeToBlackCoroutine());
            inMairie =!inMairie;
            player.transform.position = positiondansmairie;
            Deplacement.enMenu = true;
            Deplacement.enMenu = false;
            mairie.panel.SetActive(false);
            mairie.open = false;
        }
        else
        {
            StartCoroutine(FadeToBlackCoroutine());
            inMairie = !inMairie;
            player.transform.position = positiondevantmairie;
            Deplacement.enMenu = true;
            Deplacement.enMenu = false;
            mairie.panel.SetActive(false);
            mairie.open = false;
        }
    }
    public void EnterMairie()
    {
        
        StartCoroutine(FadeToBlackCoroutine());
        player.transform.position = positiondansmairie;
        Deplacement.enMenu = true;
        Deplacement.enMenu = false;
        mairie.panel.SetActive(false);
        mairie.open = false;
        //loadingOperation = SceneManager.LoadSceneAsync("Mer i");
        //SceneManager.LoadScene("Mer i");
    }

    public void EnterIsland()
    {
        StartCoroutine(FadeToBlackCoroutine());
        player.transform.position = positiondevantmairie;
        Deplacement.enMenu = true;
        Deplacement.enMenu = false;
        mairie.panel.SetActive(false);
        mairie.open = false;
        //loadingOperation = SceneManager.LoadSceneAsync("Island");

        //SceneManager.LoadScene("Island");
    }
    private IEnumerator FadeToBlackCoroutine()
    {
        float fade = 1f;
        loadingScreen.SetActive(true);
        do
        {
            Color c = loadingScreen.GetComponentInChildren<Image>().color;
            c.a = Mathf.Lerp(0.0f, 2.0f,fade);
            loadingScreen.GetComponentInChildren<Image>().color = c;
            fade -= 0.5f * Time.deltaTime;
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
