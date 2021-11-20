using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBoutonExemple : MonoBehaviour
{
    public GameObject boutonLivre;

    public void fonctionDuBouton()
    {
        boutonLivre.SetActive(false);
    }
}
