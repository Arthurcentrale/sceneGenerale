using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class drag3 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Inventaire Inventaire_script;
	public Inventaire inventaire; 
    
    void Start ()
    {
        Inventaire_script = GameObject.Find("Inventory").GetComponent<Inventaire> ();
		//inventaire = inventaire.GetComponent<Inventaire>();
		/*
		for (k=1;k<11;k++){ //desactivation collision
			transform.parent.parent.GetChild(k).GetChild(0).GetComponent<BoxCollider2D> ().enabled = false;


		}
		*/
    }

    //Transform parentToReturnTo = null;
    Vector3 positionOrigine;
    int i;
	int j;
	int k;
	int survol;



    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        //parentToReturnTo = this.transform.parent.parent;
        positionOrigine = this.transform.parent.position;
		//survol=0;
		/*
		for (k=1;k<11;k++){ //desactivation collision
			transform.parent.parent.GetChild(k).GetChild(0).GetComponent<BoxCollider2D> ().enabled = false;


		}
		*/
        //GetComponent<BoxCollider2D> ().enabled = false;
	
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        this.transform.parent.position = eventData.position;
		//survol=1;
		//GetComponent<BoxCollider2D> ().enabled = true;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        //this.transform.parent.SetParent(parentToReturnTo);
        //GetComponent<BoxCollider2D> ().enabled = true; // on réactive la collision
		/*for (k=1;k<11;k++){ //desactivation collision
			transform.parent.parent.GetChild(k).GetChild(0).GetComponent<BoxCollider2D> ().enabled = true;


		}
		*/
		//survol=0;
		
		
        this.transform.parent.position = positionOrigine;
    }
	
	
    public void OnTriggerEnter2D (Collider2D collider) 
        {
		//Debug.Log(collider.transform.parent.name);
	Debug.Log("collision");
	i=collider.transform.parent.GetSiblingIndex();
	j=transform.parent.GetSiblingIndex();

	print("collision");
	Debug.Log(i);
	Debug.Log(j);
	
	/*
	print(i);print(j);
	print(transform.parent.GetChild(1).GetComponent<Text>().text);
	print(collider.transform.parent.GetChild(1).GetComponent<Text>().text);
	*/

	

	if(i>j){ // permet une seule interversion
	if(((i>4)&&(j>4)&&(i<11)&&(j<11))||((i<5)&&(j<5)&&(i>0)&&(j>0))){ // cas 2 objets non favoris ou 2 favoris
	if(((transform.parent.GetChild(1).GetComponent<Text>().text!="")&&(collider.transform.parent.GetChild(1).GetComponent<Text>().text!=""))&&(transform.parent.GetChild(1).GetComponent<Text>().text!=collider.transform.parent.GetChild(1).GetComponent<Text>().text))
	{// empeche les interversions entre 2 objets identiques ou avec une case vide

	/*
	int k=Inventaire_script.Slot[i].Amount;
	Item item=Inventaire_script.Slot[i].Item;
	Sprite Intermediaire0= transform.GetComponent<Image>().sprite;
	string Intermediaire1= transform.parent.GetChild(1).GetComponent<Text>().text;
	string Intermediaire2= transform.parent.GetChild(2).GetComponent<Text>().text;

	Inventaire_script.Slot[i].Amount=Inventaire_script.Slot[j].Amount;
	Inventaire_script.Slot[i].Item=Inventaire_script.Slot[j].Item;
	transform.GetComponent<Image>().sprite=collider.transform.GetComponent<Image>().sprite;
	transform.parent.GetChild(1).GetComponent<Text>().text=collider.transform.parent.GetChild(1).GetComponent<Text>().text;
	transform.parent.GetChild(2).GetComponent<Text>().text=collider.transform.parent.GetChild(2).GetComponent<Text>().text;

	Inventaire_script.Slot[j].Amount=k;
	Inventaire_script.Slot[j].Item=item;
	collider.transform.GetComponent<Image>().sprite=Intermediaire0;
	collider.transform.parent.GetChild(1).GetComponent<Text>().text=Intermediaire1;
	collider.transform.parent.GetChild(2).GetComponent<Text>().text=Intermediaire2;

	*/

	//on effectue le changement
	ItemAmount transition=Inventaire_script.Slot[i];
	Inventaire_script.Slot[i]=Inventaire_script.Slot[j];
	Inventaire_script.Slot[j]=transition;

	}
	}
	if((j<5)&&(i>4)){// un non favoris vers un favoris
	//print("test1");
	/*
	if((collider.transform.parent.GetChild(1).GetComponent<Text>().text!="")&&(transform.parent.GetChild(1).GetComponent<Text>().text!=collider.transform.parent.GetChild(1).GetComponent<Text>().text))
	{
	//print("test2");

	int k=Inventaire_script.Slot[i].Amount;
    Item item=Inventaire_script.Slot[i].Item;
	Sprite Intermediaire0= transform.GetComponent<Image>().sprite;
	string Intermediaire1= transform.parent.GetChild(1).GetComponent<Text>().text;
	string Intermediaire2= transform.parent.GetChild(2).GetComponent<Text>().text;
	//Debug.Log(Intermediaire1);
	//Debug.Log(Intermediaire2);

	Inventaire_script.Slot[i].Amount=Inventaire_script.Slot[j].Amount;
    Inventaire_script.Slot[i].Item=Inventaire_script.Slot[j].Item;
	transform.GetComponent<Image>().sprite=collider.transform.GetComponent<Image>().sprite;
	transform.parent.GetChild(1).GetComponent<Text>().text=collider.transform.parent.GetChild(1).GetComponent<Text>().text;
	transform.parent.GetChild(2).GetComponent<Text>().text=collider.transform.parent.GetChild(2).GetComponent<Text>().text;
	
	Inventaire_script.Slot[j].Amount=k;
    Inventaire_script.Slot[j].Item=item;
	collider.transform.GetComponent<Image>().sprite=Intermediaire0;
	collider.transform.parent.GetChild(1).GetComponent<Text>().text=Intermediaire1;
	collider.transform.parent.GetChild(2).GetComponent<Text>().text=Intermediaire2;

	//Debug.Log(collider.transform.parent.GetChild(1).GetComponent<Text>().text);
	//Debug.Log(collider.transform.parent.GetChild(2).GetComponent<Text>().text);

	//Debug.Log(transform.parent.GetChild(1).GetComponent<Text>().text);
	//Debug.Log(transform.parent.GetChild(2).GetComponent<Text>().text);

	if(collider.transform.parent.GetChild(1).GetComponent<Text>().text==""){
	//int n = int.Parse(transform.parent.parent.GetChild(12).GetComponent<Text>().text);
	int nrSlot = collider.transform.parent.GetSiblingIndex();
	print(nrSlot);
	/*print(n);
            n -= 1;
	    if(n<4){
		n=4;
		}
		print(n);
        
            Inventaire_script.UpdateN(12, n.ToString());
            

                for (i = nrSlot; i < 11; i++)
                {

                    Inventaire_script.Slot[i] = Inventaire_script.Slot[i + 1];
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = GameObject.Find("Inventory").transform.GetChild(0).GetChild(i + 1).GetChild(1).GetComponent<Text>().text;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = GameObject.Find("Inventory").transform.GetChild(0).GetChild(i + 1).GetChild(2).GetComponent<Text>().text;
                    transform.parent.parent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = transform.parent.parent.GetChild(i + 1).GetChild(0).GetComponent<Image>().sprite;
                }
		}

	for (i = 1; i < 11; i++) { 
        Inventaire_script.UpdateTXT2(i,Inventaire_script.Slot[i].Amount.ToString());
        Inventaire_script.UpdateTXT1(i, transform.parent.parent.GetChild(i).GetChild(1).GetComponent<Text>().text);        
        }
	}*/




	}

  	}
	
	}
/*
	    void RetirerInventaire2(Item item, int Amount) //On veut retirer Amount items
    {
        if (CountItem(item.ItemName) < Amount) // on vérifie qu'il y a bien plus que Amount items dans l'inventaire
        {
            Debug.Log("Pas assez pour enlever");
        }
        else
        {
            int x = Amount; // x représente le total d'items enlevés dans l'inventaire
            int i = GameObject.Find("Inventory").transform.GetChild(0).childCount-2;
            int j =i;
            int k;
            while (x != 0) // tant qu'on a pas tout enlevé, on parcourt l'inventaire
            {
                if (inventaire.Slot[i].Item == item) // si c'est le bon item
                {
                    if (inventaire.Slot[i].Amount < x) // Si il y a pas assez d'item dans ce slot pour tout enlever,
                    {
                        x -= inventaire.Slot[i].Amount; // et il ne reste plus que x-amount a enlever
                        inventaire.Slot[i].Amount = 0;
                        // on enleve tout ce qu'il y a dans ce slot
                        for (k=i;k<j;k++)
                        {
                            inventaire.Slot[k]=inventaire.Slot[k+1];
                           
                        }
                        i--;
                        
                    }
                    else // si il y a assez de place
                    {
                        inventaire.Slot[i].Amount -= x; // on enleve tout
                        x = 0;
                        GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text=inventaire.Slot[i].Amount.ToString();
                    }
                }
                i--;
            }
        }
    }

	    void AjouterFavoris(Item item, int Amount) //On ajoute Amount items dans l'inventaire
    {
            int i = 1; // pour parcourir l'inventaire
            int x = Amount; // le total d'objet à placer
            while ((x != 0)||(i<5)) // tant que l'on a pas tout placé
            {
                if (inventaire.Slot[i].Item == item) // si on a le bon item dans l'inventaire
                {
                    if (x + inventaire.Slot[i].Amount * item.Weight > 5) // si on doit placer trop d'item par rapport a la place qu'il reste dans ce slot
                    {
                        x -= 5 / item.Weight - inventaire.Slot[i].Amount;
                        inventaire.Slot[i].Amount = 5 / item.Weight; // on place ce que l'on peut et on continue de parcourir la liste pour placer le reste
                        GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text=inventaire.Slot[i].Amount.ToString();
                    }
                    else // si on a assez de place , on place tout
                    {
                        inventaire.Slot[i].Amount += x;
                        GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text=inventaire.Slot[i].Amount.ToString();
                        x = 0;

                    }
                }
                if (inventaire.Slot[i].Item.ItemName == "Vide") // Si l'emplacement est vide, on met les items la
                {
                    inventaire.Slot[i].Item = item;
                    inventaire.Slot[i].Amount += x;
                    x = 0;

                  
                }
                i++;

            }
        }
    }*/


    
}
