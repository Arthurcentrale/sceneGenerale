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
	if((j<5)&&(i>4)||(i<5)&&(j>4)){// un non favoris vers un favoris et inversement
	if (collider.transform.parent.GetChild(1).GetComponent<Text>().text!=""){
    ItemAmount transition2=Inventaire_script.Slot[i];
	Inventaire_script.Slot[i]=Inventaire_script.Slot[j];
	Inventaire_script.Slot[j]=transition2;}

	}

  	}
	
	}



    
}
