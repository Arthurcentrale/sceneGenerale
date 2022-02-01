using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabanon : MonoBehaviour
{
    public Chauffage chauffage;

    void OnMouseDown(){
        chauffage.AfficherChauffage();
    }
}
