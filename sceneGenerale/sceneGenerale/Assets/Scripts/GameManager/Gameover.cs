using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    AsyncOperation loadingOperation;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.environnementManager.qualiteAir == 0 || GameManager.environnementManager.qualiteEau == 0 || GameManager.environnementManager.qualiteSol == 0 || GameManager.socialManager.qualiteDeVie == 0)
        {
            ChangerScene();
        }
    }


    void ChangerScene()
    {
        loadingOperation = SceneManager.LoadSceneAsync("EndScene");
    }
}
