using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class drag2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Inventaire Inventaire_script;
    
    void Start ()
    {
        Inventaire_script = GameObject.Find("Inventory").GetComponent<Inventaire> ();

    }

    //Transform parentToReturnTo = null;
    Vector3 positionOrigine;
    int i;



    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        //parentToReturnTo = this.transform.parent.parent;
        positionOrigine = this.transform.parent.position;
        //this.transform.parent.SetParent(this.transform.parent.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        this.transform.parent.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        //this.transform.parent.SetParent(parentToReturnTo);
        

        this.transform.parent.position = positionOrigine;
    	}
	
    void OnTriggerEnter2D (Collider2D collider) 
        {

	//Debug.Log(collider.transform.parent.name);

	int i=collider.transform.parent.GetSiblingIndex();
	int j=transform.parent.GetSiblingIndex();

	/*
	print(i);print(j);
	print(transform.parent.GetChild(1).GetComponent<Text>().text);
	print(collider.transform.parent.GetChild(1).GetComponent<Text>().text);
	*/
	
	if(i>j){
	if(((i>4)&&(j>4)&&(i<11)&&(j<11))||((i<5)&&(j<5)&&(i>0)&&(j>0))){
	if(((transform.parent.GetChild(1).GetComponent<Text>().text!="")&&(collider.transform.parent.GetChild(1).GetComponent<Text>().text!=""))&&(transform.parent.GetChild(1).GetComponent<Text>().text!=collider.transform.parent.GetChild(1).GetComponent<Text>().text))
	{
	int k=Inventaire_script.Slot[i].Amount;
        Sprite Intermediaire0= transform.GetComponent<Image>().sprite;
	string Intermediaire1= transform.parent.GetChild(1).GetComponent<Text>().text;
	string Intermediaire2= transform.parent.GetChild(2).GetComponent<Text>().text;

	Inventaire_script.Slot[i].Amount=Inventaire_script.Slot[j].Amount;
	transform.GetComponent<Image>().sprite=collider.transform.GetComponent<Image>().sprite;
	transform.parent.GetChild(1).GetComponent<Text>().text=collider.transform.parent.GetChild(1).GetComponent<Text>().text;
	transform.parent.GetChild(2).GetComponent<Text>().text=collider.transform.parent.GetChild(2).GetComponent<Text>().text;

	Inventaire_script.Slot[j].Amount=k;
	collider.transform.GetComponent<Image>().sprite=Intermediaire0;
	collider.transform.parent.GetChild(1).GetComponent<Text>().text=Intermediaire1;
	collider.transform.parent.GetChild(2).GetComponent<Text>().text=Intermediaire2;
	//this.transform.parent.position = positionOrigine;
	}
	}
	if((j<5)&&(i>4)){
	//print("test1");
	if((collider.transform.parent.GetChild(1).GetComponent<Text>().text!="")&&(transform.parent.GetChild(1).GetComponent<Text>().text!=collider.transform.parent.GetChild(1).GetComponent<Text>().text))
	{
	//print("test2");
	int k=Inventaire_script.Slot[i].Amount;
	Sprite Intermediaire0= transform.GetComponent<Image>().sprite;
	string Intermediaire1= transform.parent.GetChild(1).GetComponent<Text>().text;
	string Intermediaire2= transform.parent.GetChild(2).GetComponent<Text>().text;
	Debug.Log(Intermediaire1);
	Debug.Log(Intermediaire2);

	Inventaire_script.Slot[i].Amount=Inventaire_script.Slot[j].Amount;
	transform.GetComponent<Image>().sprite=collider.transform.GetComponent<Image>().sprite;
	transform.parent.GetChild(1).GetComponent<Text>().text=collider.transform.parent.GetChild(1).GetComponent<Text>().text;
	transform.parent.GetChild(2).GetComponent<Text>().text=collider.transform.parent.GetChild(2).GetComponent<Text>().text;
	
	Inventaire_script.Slot[j].Amount=k;
	collider.transform.GetComponent<Image>().sprite=Intermediaire0;
	collider.transform.parent.GetChild(1).GetComponent<Text>().text=Intermediaire1;
	collider.transform.parent.GetChild(2).GetComponent<Text>().text=Intermediaire2;

	Debug.Log(collider.transform.parent.GetChild(1).GetComponent<Text>().text);
	Debug.Log(collider.transform.parent.GetChild(2).GetComponent<Text>().text);

	Debug.Log(transform.parent.GetChild(1).GetComponent<Text>().text);
	Debug.Log(transform.parent.GetChild(2).GetComponent<Text>().text);

	if(collider.transform.parent.GetChild(1).GetComponent<Text>().text==""){
	int n = int.Parse(transform.parent.parent.GetChild(12).GetComponent<Text>().text);
	int nrSlot = collider.transform.parent.GetSiblingIndex();
	print(nrSlot);
	//print(n);
            n -= 1;
	    if(n<4){
		n=4;
		}
		print(n);

            Inventaire_script.UpdateN(12, n.ToString());


                for (i = nrSlot; i < 11; i++)
                {

                    Inventaire_script.Slot[i].Amount = Inventaire_script.Slot[i + 1].Amount;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = GameObject.Find("Inventory").transform.GetChild(0).GetChild(i + 1).GetChild(1).GetComponent<Text>().text;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = GameObject.Find("Inventory").transform.GetChild(0).GetChild(i + 1).GetChild(2).GetComponent<Text>().text;
                    transform.parent.parent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = transform.parent.parent.GetChild(i + 1).GetChild(0).GetComponent<Image>().sprite;
                }
		}

	for (i = 1; i < 11; i++) { 
        Inventaire_script.UpdateTXT2(i,Inventaire_script.Slot[i].Amount.ToString());
        Inventaire_script.UpdateTXT1(i, transform.parent.parent.GetChild(i).GetChild(1).GetComponent<Text>().text);        
        }
	}
	}

  	}
	
	}
	
	/*void Update()
    {	
	int k;
	int i;
	int n = int.Parse(transform.parent.parent.GetChild(11).GetComponent<Text>().text);
	for (k=4;k<9;k++){
        if (Inventaire_script.Slot[k].Amount < 1)
        //Inventaire_script.Slot[k] = 0;
        {
            //print(n);
            n -= 1;
	    if(n<4){
		n=4;}
            //print(n);
	 	Inventaire_script.UpdateN(11, n.ToString());

            Inventaire_script.UpdateN(11, n.ToString());

            if (k < n)
            {
                for (i =k ; k < 9; i++)
                {

                    Inventaire_script.Slot[i].Amount = Inventaire_script.Slot[i + 1].Amount;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = GameObject.Find("Inventory").transform.GetChild(0).GetChild(i + 1).GetChild(1).GetComponent<Text>().text;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = GameObject.Find("Inventory").transform.GetChild(0).GetChild(i + 1).GetChild(2).GetComponent<Text>().text;
                    transform.parent.parent.GetChild(k).GetChild(0).GetComponent<Image>().sprite = transform.parent.parent.GetChild(i + 1).GetChild(0).GetComponent<Image>().sprite;
    	}
	}
	}
	}
	}*/
    
}