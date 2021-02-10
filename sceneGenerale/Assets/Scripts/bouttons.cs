using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bouttons : MonoBehaviour
{
    // Start is called before the first frame update
    public void Exit()
    {
        transform.parent.GetComponent<Canvas> ().enabled = false;
        Debug.Log("fait");
    }

    public void InventaireOn()
    {
        GameObject.Find("Inventory").transform.GetComponent<Canvas> ().enabled = true;
    }

}
