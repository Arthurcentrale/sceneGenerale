using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Random;
using System.Linq; //Pour pouvoir créer une liste d'objets ayant le même nom
using System;

public class Maladie : MonoBehaviour
{
    // Les probas
    private float probaMaladieChêne=0.3f;
    private float probaMaladieHêtre=0.1f;
    private float probaMaladiePin=0.3f;
    private float probaMaladieDouglas=0.15f;
    private float probaMaladieBouleau=0.15f;
    // je vais faire du cas par cas pour la phase initiale de la maladie, je vois pas trop comment faire autrement.
    private bool maladieSurChêne=false;
    private bool maladieSurHêtre=false;
    private bool maladieSurPin=false;
    private bool maladieSurDouglas=false;
    private bool maladieSurBouleau=false;
    

    //Scripts
    private ArbreManager arbreManager; 

    //Les variables
    private GameObject imposteur;
    private GameObject premierArbreMalade;
    private float distancemini;
    private GameObject gameObjectProche;
    private int nombreArbresMalades;
    private GameObject prochainArbreMalade;
    public bool maladieEnCours=false;
    public string essenceMalade="aucune";
    private Transform dossierArbres;
    
    void Start(){
        //arbreManager = GameObject.Find("Game Manager").GetComponent<ArbreManager>();
        //var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Chene(Clone)"); // la liste contient aussi les objects inactifs qui portent ce nom, il faudra faire attention
        //print("nombre de chenes" + objects.Count()); //après test ça a l'air de bien marcher
        //DéclencherMaladie();
        dossierArbres=GameObject.Find("Arbres").transform;
    }
    
    public void DéclencherMaladie(){
        // Choix de l'essence de l'arbre qui sera contaminée 
        arbreManager = GameObject.Find("Game Manager").GetComponent<ArbreManager>();
        var rand = UnityEngine.Random.Range(0f,1f);
        //print(rand);
        //On transforme nos bools en conséquence
        if (rand<probaMaladieChêne){
            // maladieSurChêne=true;
             print("maladieSurChêne");
            var objects1 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Chene(Clone)");
            var objects2 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Chene");
            var objects= objects1.Concat(objects2);
            if (objects.Count()!=0){ //Pour éviter d'avoir des erreurs qui empêchent d'effectuer la sgarde
                premierArbreMalade=objects.ElementAt(0);
                RendreArbreMalade(premierArbreMalade,"Chene");
                //premierArbreMalade.name="Chene Malade";
                nombreArbresMalades=1;
                maladieEnCours=true;
                essenceMalade="chene";
                prochainArbreMalade=premierArbreMalade;
                while(nombreArbresMalades<=(0.25*(objects.Count()))){
                    prochainArbreMalade=PlusProcheGameObjectAvecNom(prochainArbreMalade,"Chene(Clone)","Chene","Chene Frele","Chene Frele(Clone)");
                    nombreArbresMalades+=1;
                    RendreArbreMalade(prochainArbreMalade,"Chene");
                    //prochainArbreMalade.name="Chene Malade";
            }
            }
        }
        if (probaMaladieChêne<rand && rand<probaMaladieChêne+probaMaladieHêtre){
            // maladieSurHêtre=true;
             print("maladieSurHêtre");
            var objects1 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Hetre(Clone)");
            var objects2 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Hetre");
            var objects= objects1.Concat(objects2);
            if (objects.Count()!=0){
                premierArbreMalade=objects.ElementAt(0);
                RendreArbreMalade(premierArbreMalade,"Hetre");
                //premierArbreMalade.name="Hetre Malade";
                nombreArbresMalades=1;
                maladieEnCours=true;
                essenceMalade="hetre";
                prochainArbreMalade=premierArbreMalade;
                while(nombreArbresMalades<=(0.25*(objects.Count()))){
                    prochainArbreMalade=PlusProcheGameObjectAvecNom(prochainArbreMalade,"Hetre(Clone)","Hetre","Hetre Frele","Hetre Frele(Clone)");
                    nombreArbresMalades+=1;
                    RendreArbreMalade(prochainArbreMalade,"Hetre");
                    //premierArbreMalade.name="Hetre Malade";
            }
            }
        }
        if (rand>probaMaladieChêne+probaMaladieHêtre && rand<probaMaladieChêne+probaMaladieHêtre+probaMaladiePin){
            // maladieSurPin=true;
             print("maladieSurPin");
            var objects1 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Pin(Clone)");
            var objects2 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Pin");
            var objects= objects1.Concat(objects2);
            if (objects.Count()!=0){
                premierArbreMalade=objects.ElementAt(0);
                RendreArbreMalade(premierArbreMalade,"Pin");
                //premierArbreMalade.name="Pin Malade";
                nombreArbresMalades=1;
                maladieEnCours=true;
                essenceMalade="pin";
                prochainArbreMalade=premierArbreMalade;
                while(nombreArbresMalades<=(0.25*(objects.Count()))){
                    prochainArbreMalade=PlusProcheGameObjectAvecNom(prochainArbreMalade,"Pin(Clone)","Pin","Pin Frele","Pin Frele(Clone)");
                    nombreArbresMalades+=1;
                    RendreArbreMalade(prochainArbreMalade,"Pin");
                    //premierArbreMalade.name="Pin Malade";
            }
            }
        }
        if (rand>probaMaladieChêne+probaMaladieHêtre+probaMaladiePin && rand<probaMaladieChêne+probaMaladieHêtre+probaMaladiePin+probaMaladieDouglas){
            // maladieSurDouglas=true;
             print("maladieSurDouglas");
            var objects1 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Douglas(Clone)");
            var objects2 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Douglas");
            var objects= objects1.Concat(objects2);
            if (objects.Count()!=0){
                premierArbreMalade=objects.ElementAt(0);
                RendreArbreMalade(premierArbreMalade,"Douglas");
                //premierArbreMalade.name="Douglas Malade";
                nombreArbresMalades=1;
                maladieEnCours=true;
                essenceMalade="douglas";
                prochainArbreMalade=premierArbreMalade;
                while(nombreArbresMalades<=(0.25*(objects.Count()))){
                    prochainArbreMalade=PlusProcheGameObjectAvecNom(prochainArbreMalade,"Douglas(Clone)","Douglas","Douglas Frele","Douglas Frele(Clone)");
                    nombreArbresMalades+=1;
                    RendreArbreMalade(prochainArbreMalade,"Douglas");
                    //premierArbreMalade.name="Douglas Malade";
                }
            }
        }
        if (rand>1-probaMaladieBouleau){
            // maladieSurBouleau=true;
             print("maladieSurBouleau");
            var objects1 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Bouleau(Clone)");
            var objects2 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Bouleau");
            var objects= objects1.Concat(objects2);
            if (objects.Count()!=0){
                premierArbreMalade=objects.ElementAt(0);
                RendreArbreMalade(premierArbreMalade,"Bouleau");
                //premierArbreMalade.name="Bouleau Malade";
                nombreArbresMalades=1;
                maladieEnCours=true;
                essenceMalade="bouleau";
                prochainArbreMalade=premierArbreMalade;
                while(nombreArbresMalades<=(0.25*(objects.Count()))){
                    prochainArbreMalade=PlusProcheGameObjectAvecNom(prochainArbreMalade,"Bouleau(Clone)","Bouleau","Bouleau Frele","Bouleau Frele(Clone)");
                    nombreArbresMalades+=1;
                    RendreArbreMalade(prochainArbreMalade,"Bouleau");
                    //premierArbreMalade.name="Bouleau Malade";
            }
            }
        }
        
        



    }


    public void ActualisationMaladie(string essenceMalade) // toutes les 3 heures
    
    {
        if (essenceMalade=="chene" && maladieEnCours){
            premierArbreMalade=GameObject.Find("Chene Malade(Clone)"); //on prend un arbre malade random
            if (premierArbreMalade!=null){ //nous évite tout plein d'erreurs chiantes
                prochainArbreMalade=PlusProcheGameObjectAvecNom(premierArbreMalade,"Chene(Clone)","Chene","Chene Frele","Chene Frele(Clone)"); //on contamine un arbre à côté du précédent
                //Comme prochainArbreMalade aura changé de nom, il ne sera plus dans la liste des "Chene(Clone)" donc on ne contaminera pas le même arbre en boucle
                if (prochainArbreMalade!=null){
                    RendreArbreMalade(prochainArbreMalade,"Chene");
                }
                //premierArbreMalade.name="Chene Malade";
            }
            {

            }
        }
        if (essenceMalade=="hetre" && maladieEnCours){
            premierArbreMalade=GameObject.Find("Hetre Malade(Clone)"); //on prend un arbre malade random
            if (premierArbreMalade!=null){
                prochainArbreMalade=PlusProcheGameObjectAvecNom(premierArbreMalade,"Hetre(Clone)","Hetre","Hetre Frele","Hetre Frele(Clone)"); //on contamine un arbre à côté du précédent
                if (prochainArbreMalade!=null){
                    RendreArbreMalade(prochainArbreMalade,"Hetre");
                }
                //premierArbreMalade.name="Hetre Malade";
            }
        }
        if (essenceMalade=="pin" && maladieEnCours){
            premierArbreMalade=GameObject.Find("Pin Malade(Clone)"); //on prend un arbre malade random
            if (premierArbreMalade!=null){
                prochainArbreMalade=PlusProcheGameObjectAvecNom(premierArbreMalade,"Pin(Clone)","Pin","Pin Frele","Pin Frele(Clone)"); //on contamine un arbre à côté du précédent
                if (prochainArbreMalade!=null){
                    RendreArbreMalade(prochainArbreMalade,"Pin");
                }
                //premierArbreMalade.name="Pin Malade";
            }
        }
        if (essenceMalade=="douglas" && maladieEnCours){
            premierArbreMalade=GameObject.Find("Douglas Malade(Clone)"); //on prend un arbre malade random
            if (premierArbreMalade!=null){  
                prochainArbreMalade=PlusProcheGameObjectAvecNom(premierArbreMalade,"Douglas(Clone)","Douglas","Douglas Frele","Douglas Frele(Clone)"); //on contamine un arbre à côté du précédent
                if (prochainArbreMalade!=null){
                    RendreArbreMalade(prochainArbreMalade,"Douglas");
                }
                //premierArbreMalade.name="Douglas Malade";
            }
        }
        if (essenceMalade=="bouleau" && maladieEnCours){
            premierArbreMalade=GameObject.Find("Bouleau Malade(Clone)"); //on prend un arbre malade random
            if (premierArbreMalade!=null){
                prochainArbreMalade=PlusProcheGameObjectAvecNom(premierArbreMalade,"Bouleau(Clone)","Bouleau","Bouleau Frele","Bouleau Frele(Clone)"); //on contamine un arbre à côté du précédent
                if (prochainArbreMalade!=null){
                    RendreArbreMalade(prochainArbreMalade,"Bouleau");
                }
                //premierArbreMalade.name="Bouleau Malade";
            }
        }
    }

    private GameObject PlusProcheGameObjectAvecNom(GameObject caca, string name1, string name2, string name3, string name4){ //pardon
        var objectsprout = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == name1);
        var objectspipi = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == name2);
        var objectscaca1=objectsprout.Concat(objectspipi);
        var objectsprout2 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == name3);
        var objectspipi2 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == name4);
        var objectscaca2=objectsprout2.Concat(objectspipi2);
        var objectscaca=objectscaca1.Concat(objectscaca2);
        if (objectscaca.Count()<=1){
            return(null); //me demande pas pourquoi mais ça marche, quand le count est à 1 ya plus d'arbre non malade donc juste je retourne null et je vis ma vie
        }
        Vector3 prout=caca.transform.position;
        distancemini=(Vector3.Distance(caca.transform.position,objectscaca.ElementAt(0).transform.position));
        gameObjectProche=objectscaca.ElementAt(0);
        for (int i=1; i<objectscaca.Count(); i++){
            if (distancemini>Vector3.Distance(caca.transform.position,objectscaca.ElementAt(i).transform.position)){
                distancemini=Vector3.Distance(caca.transform.position,objectscaca.ElementAt(i).transform.position);
                gameObjectProche=objectscaca.ElementAt(i);
            }
        }
        return(gameObjectProche);

    }

    private void RendreArbreMalade(GameObject arbre, string nom){
        if (nom=="Chene" && arbre!=null){
            Vector3 coord=arbre.transform.position;
            Quaternion rot = arbreManager.cheneRobuste.transform.rotation;
            Instantiate(arbreManager.cheneMalade, coord, rot,dossierArbres);
            Destroy(arbre);

        }
        if (nom=="Hetre"&& arbre!=null){
            Vector3 coord=arbre.transform.position;
            Quaternion rot = arbreManager.hetreRobuste.transform.rotation;
            Instantiate(arbreManager.hetreMalade, coord, rot,dossierArbres);
            Destroy(arbre);
        }
        if (nom=="Pin"&& arbre!=null){
            Vector3 coord=arbre.transform.position;
            Quaternion rot = arbreManager.pinRobuste.transform.rotation;
            Instantiate(arbreManager.pinMalade, coord, rot,dossierArbres);
            Destroy(arbre);
        }
        if (nom=="Douglas"&& arbre!=null){
            Vector3 coord=arbre.transform.position;
            Quaternion rot = arbreManager.douglasRobuste.transform.rotation;
            Instantiate(arbreManager.douglasMalade, coord, rot,dossierArbres);
            Destroy(arbre);
        }
        if (nom=="Bouleau"&& arbre!=null){
            Vector3 coord=arbre.transform.position;
            Quaternion rot = arbreManager.bouleauRobuste.transform.rotation;
            Instantiate(arbreManager.bouleauMalade, coord, rot,dossierArbres);
            Destroy(arbre);
        }
    }




    public void FonctionQuiSeDéclencheÀMinuit(){
        if ((!maladieEnCours)){
            DéclencherMaladie();
        }
        
    }


    public void VérifierMaladie(){
        GameObject[] ListeGO = GameObject.FindGameObjectsWithTag("Arbre");
        maladieEnCours=false;
        
        foreach(GameObject item in ListeGO){
            if (item.name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0){ //S'il reste un arbre malade (marche aussi avec les arbustes!!), alors la maladie est toujours en cours
            maladieEnCours=true; 
            }
        }
    }
        

    
     
    // j'introduis une norme euclidienne pour les distances.
    // soit je fais une liste des distances de l'arbre malade initial aux autres arbres puis je garde que le quart des distances les plus faibles
    //soit je fais un cercle de plus en plus grand jusqu'à ce que j'aie 1/4 des arbres (plus chiant je pense)
}
        