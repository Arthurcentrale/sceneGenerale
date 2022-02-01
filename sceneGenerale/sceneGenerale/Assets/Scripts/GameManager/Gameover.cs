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
        if (EnvironnementManager.instance.qualiteAir == 0f)
        {
            ChangerScene();
        }
    }


    void ChangerScene()
    {
        loadingOperation = SceneManager.LoadSceneAsync("EndScene");
    }
}
