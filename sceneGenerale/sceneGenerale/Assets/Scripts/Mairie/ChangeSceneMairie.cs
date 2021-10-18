using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneMairie : MonoBehaviour
{
    // Start is called before the first frame update
    public void EnterMairie()
    {
        SceneManager.LoadScene("Mer i");
    }

    public void EnterIsland()
    {
        SceneManager.LoadScene("Island");
    }
}
