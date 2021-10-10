using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialManager
{
    public int quantiteNourriture { get; set; }
    public int ecoSensibilisation { get; set; }
    public int qualiteDeVie { get; set; }

    public SocialManager(int nourriture, int sensibilisation, int qualite)
    {
        this.quantiteNourriture = nourriture;
        this.ecoSensibilisation = sensibilisation;
        this.qualiteDeVie = qualite;
    }

    private void MajSocial()
    {
        //quantiteNourriture =
    }
}
