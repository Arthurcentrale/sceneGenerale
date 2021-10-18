using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Start is called before the first frame update
    public void BoutonJouer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //voir t=9min pour plus d'infos https://www.youtube.com/watch?v=zc8ac_qUXQY&t=590s&ab_channel=Brackeys
    }

    public void BoutonQuitter()
    {
         
        Application.Quit();
    }
}
