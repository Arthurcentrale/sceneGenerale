using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Random;
using System.Linq; //Pour pouvoir créer une liste d'objets ayant le même nom

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
    

    //Les variables
    private GameObject premierArbreMalade;
    private float distancemini;
    private GameObject gameObjectProche;
    private int nombreArbresMalades;
    private GameObject prochainArbreMalade;
    
    void Start(){
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Chene(Clone)"); // la liste contient aussi les objects inactifs qui portent ce nom, il faudra faire attention
        print("nombre de chenes" + objects.Count()); //après test ça a l'air de bien marcher
        DéclencherMaladie();

    }
    
    public void DéclencherMaladie(){
        // Choix de l'essence de l'arbre qui sera contaminée 
        var rand = Random.Range(0f,1f);
        print(rand);
        //On transforme nos bools en conséquence
        if (rand<probaMaladieChêne){
            maladieSurChêne=true;
            print("maladieSurChêne");
            var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Chene(Clone)");
            premierArbreMalade=objects.ElementAt(0);
            premierArbreMalade.name="CheneMalade";
            nombreArbresMalades=1;
            prochainArbreMalade=premierArbreMalade;
            while(nombreArbresMalades<=(0.25*(objects.Count()))){
                prochainArbreMalade=PlusProcheGameObjectAvecNom(prochainArbreMalade,"Chene(Clone)");
                nombreArbresMalades+=1;
                premierArbreMalade.name="CheneMalade";
            }
        }
        if (probaMaladieChêne<rand && rand<probaMaladieChêne+probaMaladieHêtre){
            maladieSurHêtre=true;
            print("maladieSurHêtre");
            var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Hetre(Clone)");
            premierArbreMalade=objects.ElementAt(0);
            premierArbreMalade.name="HetreMalade";
            nombreArbresMalades=1;
            prochainArbreMalade=premierArbreMalade;
            while(nombreArbresMalades<=(0.25*(objects.Count()))){
                prochainArbreMalade=PlusProcheGameObjectAvecNom(prochainArbreMalade,"Hetre(Clone)");
                nombreArbresMalades+=1;
                premierArbreMalade.name="HetreMalade";
            }
        }
        if (rand>probaMaladieChêne+probaMaladieHêtre && rand<probaMaladieChêne+probaMaladieHêtre+probaMaladiePin){
            maladieSurPin=true;
            print("maladieSurPin");
            var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Pin(Clone)");
            premierArbreMalade=objects.ElementAt(0);
            premierArbreMalade.name="PinMalade";
            nombreArbresMalades=1;
            prochainArbreMalade=premierArbreMalade;
            while(nombreArbresMalades<=(0.25*(objects.Count()))){
                prochainArbreMalade=PlusProcheGameObjectAvecNom(prochainArbreMalade,"Pin(Clone)");
                nombreArbresMalades+=1;
                premierArbreMalade.name="PinMalade";
            }
        }
        if (rand>probaMaladieChêne+probaMaladieHêtre+probaMaladiePin && rand<probaMaladieChêne+probaMaladieHêtre+probaMaladiePin+probaMaladieDouglas){
            maladieSurDouglas=true;
            print("maladieSurDouglas");
            var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Douglas(Clone)");
            print("object count: ");
            print(objects.Count());
            print("x0.25 : ");
            print(0.25*objects.Count());
            premierArbreMalade=objects.ElementAt(0);
            premierArbreMalade.name="DouglasMalade";
            nombreArbresMalades=1;
            prochainArbreMalade=premierArbreMalade;
            while(nombreArbresMalades<=(0.25*(objects.Count()))){
                prochainArbreMalade=PlusProcheGameObjectAvecNom(prochainArbreMalade,"Douglas(Clone)");
                nombreArbresMalades+=1;
                premierArbreMalade.name="DouglasMalade";
            }
        }
        if (rand>1-probaMaladieBouleau){
            maladieSurBouleau=true;
            print("maladieSurBouleau");
            var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Bouleau(Clone)");
            premierArbreMalade=objects.ElementAt(0);
            premierArbreMalade.name="BouleauMalade";
            nombreArbresMalades=1;
            prochainArbreMalade=premierArbreMalade;
            while(nombreArbresMalades<=(0.25*(objects.Count()))){
                prochainArbreMalade=PlusProcheGameObjectAvecNom(prochainArbreMalade,"Bouleau(Clone)");
                nombreArbresMalades+=1;
                premierArbreMalade.name="BouleauMalade";
            }
        }
        
        



    }


    public void ActualisationMaladie(string essenceMalade) // toutes les 3 heures
    //Note: je n'empêche pas d'avoir des maladies sur plusieurs essences différentes en même temps
    {
        if (essenceMalade=="Chene"){
            premierArbreMalade=GameObject.Find("CheneMalade"); //on prend un arbre malade random
            prochainArbreMalade=PlusProcheGameObjectAvecNom(premierArbreMalade,"Chene(Clone)"); //on contamine un arbre à côté du précédent
            premierArbreMalade.name="CheneMalade";
        }
        if (essenceMalade=="Hetre"){
            premierArbreMalade=GameObject.Find("HetreMalade"); //on prend un arbre malade random
            prochainArbreMalade=PlusProcheGameObjectAvecNom(premierArbreMalade,"Hetre(Clone)"); //on contamine un arbre à côté du précédent
            premierArbreMalade.name="HetreMalade";
        }
        if (essenceMalade=="Pin"){
            premierArbreMalade=GameObject.Find("PinMalade"); //on prend un arbre malade random
            prochainArbreMalade=PlusProcheGameObjectAvecNom(premierArbreMalade,"Pin(Clone)"); //on contamine un arbre à côté du précédent
            premierArbreMalade.name="PinMalade";
        }
        if (essenceMalade=="Douglas"){
            premierArbreMalade=GameObject.Find("DouglasMalade"); //on prend un arbre malade random
            prochainArbreMalade=PlusProcheGameObjectAvecNom(premierArbreMalade,"Douglas(Clone)"); //on contamine un arbre à côté du précédent
            premierArbreMalade.name="DouglasMalade";
        }
        if (essenceMalade=="Bouleau"){
            premierArbreMalade=GameObject.Find("BouleauMalade"); //on prend un arbre malade random
            prochainArbreMalade=PlusProcheGameObjectAvecNom(premierArbreMalade,"Bouleau(Clone)"); //on contamine un arbre à côté du précédent
            premierArbreMalade.name="BouleauMalade";
        }
    }

    private GameObject PlusProcheGameObjectAvecNom(GameObject caca, string name1){
        var objectscaca = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == name1);
        distancemini=(Vector3.Distance(caca.transform.position,objectscaca.ElementAt(0).transform.position));
        gameObjectProche=new GameObject();
        for (int i=1; i<objectscaca.Count(); i++){
            if (distancemini>Vector3.Distance(caca.transform.position,objectscaca.ElementAt(i).transform.position)){
                distancemini=Vector3.Distance(caca.transform.position,objectscaca.ElementAt(i).transform.position);
                gameObjectProche=objectscaca.ElementAt(i);
            }
        }
        return(gameObjectProche);

    }

    
     
    // j'introduis une norme euclidienne pour les distances.
    // soit je fais une liste des distances de l'arbre malade initial aux autres arbres puis je garde que le quart des distances les plus faibles
    //soit je fais un cercle de plus en plus grand jusqu'à ce que j'aie 1/4 des arbres (plus chiant je pense)
}
        