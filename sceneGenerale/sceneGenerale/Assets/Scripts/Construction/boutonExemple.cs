using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boutonExemple : MonoBehaviour
{
    public GameObject boutonLivre;

    public void fonctionDuBouton()
    {
        boutonLivre.SetActive(false);
    }
}
