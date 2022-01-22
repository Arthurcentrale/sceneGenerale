using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{

    private float tempsPopupVisible = 1.5f;
    
    public void popup(string message)
    {
        GetComponent<Animator>().SetTrigger("ouvrir");
        transform.GetChild(1).GetComponent<Text>().text = message;
        StartCoroutine(wait());
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(tempsPopupVisible);
        Debug.Log("coucou");
        GetComponent<Animator>().SetTrigger("fermer");
    }
}
