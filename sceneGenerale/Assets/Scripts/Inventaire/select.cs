using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class select : MonoBehaviour
{   
    //public GameObject Inventory;



    Inventaire Inventaire_script;
    public Sprite Vide, Berry_02, None, Berry_03;
    //GameObject P;

    void Start ()
    {
        Inventaire_script = GameObject.Find("Inventory").GetComponent<Inventaire> ();

        //P = transform.parent.gameObject;
    }


    // Start is called before the first frame update
    public void Selection ()
    {   
        

        // Nr Slot
        int nrSlot = transform.parent.GetSiblingIndex(); // concerne Geatan
	int nrSlot2=nrSlot;
        int n = int.Parse(transform.parent.parent.GetChild(12).GetComponent<Text>().text); // nombre de slot occupe
        int i;
        // Decremente
	
        for (i = nrSlot; i < 11; i++)
        {
            if(transform.parent.parent.GetChild(nrSlot).GetChild(1).GetComponent<Text>().text== transform.parent.parent.GetChild(i).GetChild(1).GetComponent<Text>().text)
            {
                nrSlot = i;
            }


        }

	print(nrSlot);print(nrSlot2);

        Inventaire_script.Slot[nrSlot].Amount -= 1;

        if (Inventaire_script.Slot[nrSlot].Amount < 1)
        //Inventaire_script.Slot[nrSlot] = 0;
        {
            //print(n);
            n -= 1;
	    if(n<5){
		n=5;}
            //print(n);

            Inventaire_script.UpdateN(12, n.ToString());

            if ((nrSlot < n)&&(nrSlot>4))
            {
                for (i = nrSlot; i < 12; i++)
                {

                    Inventaire_script.Slot[i].Amount = Inventaire_script.Slot[i + 1].Amount;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = GameObject.Find("Inventory").transform.GetChild(0).GetChild(i + 1).GetChild(1).GetComponent<Text>().text;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = GameObject.Find("Inventory").transform.GetChild(0).GetChild(i + 1).GetChild(2).GetComponent<Text>().text;
                    transform.parent.parent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = transform.parent.parent.GetChild(i + 1).GetChild(0).GetComponent<Image>().sprite;
                    /*
                    Inventaire_script.Slot[5] = 0;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(5).GetChild(1).GetComponent<Text>().text = " ";
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(5).GetChild(2).GetComponent<Text>().text = " ";
                    transform.parent.parent.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Vide;
                    */



                }
            }
            else {
                Inventaire_script.Slot[nrSlot].Amount = 0;
                transform.parent.parent.GetChild(nrSlot).GetChild(1).GetComponent<Text>().text = "";
                transform.parent.parent.GetChild(nrSlot).GetChild(2).GetComponent<Text>().text = "";
                transform.parent.parent.GetChild(nrSlot).GetChild(0).GetComponent<Image>().sprite = Vide;
            }
            /*
                Inventaire_script.Slot[5] = 0;
                transform.parent.parent.GetChild(5).GetChild(1).GetComponent<Text>().text = "";
                transform.parent.parent.GetChild(5).GetChild(2).GetComponent<Text>().text = "";
                transform.parent.parent.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Vide;
                Inventaire_script.UpdateTXT2(5, Inventaire_script.Slot[5].ToString());
                Inventaire_script.UpdateTXT1(5, transform.parent.parent.GetChild(5).GetChild(5).GetComponent<Text>().text);
                   */

            //



            /*
            transform.parent.GetChild(0).GetComponent<Image>().sprite = Vide;
            //transform.parent.GetChild(0).GetComponent<Image>().overrideSprite = Berry_03;
            //print(transform.parent.GetChild(0).GetComponent<Image>().sprite);
            transform.parent.GetChild(1).GetComponent<Text>().text = "";
            transform.parent.GetChild(2).GetComponent<Text>().text = "";
            */

           
        }
        /*
        if Inventaire_script.Slot[nrSlot] == 0{
            Inventaire_script.UpdateTXT(" ", Inventaire_script.Slot[nrSlot].ToString());




        }
        */
        //GameObject.Find("Inventory").transform.GetChild(0).GetChild(6).GetComponent<Text>().text = n.ToString();



        for (i = 1; i < 12; i++) { 
        Inventaire_script.UpdateTXT2(i,Inventaire_script.Slot[i].Amount.ToString());
        Inventaire_script.UpdateTXT1(i, transform.parent.parent.GetChild(i).GetChild(1).GetComponent<Text>().text);        
        }
        

        Debug.Log(transform.parent.GetSiblingIndex());
    }





    // Update is called once per frame
    void Update()
    {	/*
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
                for (i =k ; k < 12; i++)
                {

                    Inventaire_script.Slot[i].Amount = Inventaire_script.Slot[i + 1].Amount;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = GameObject.Find("Inventory").transform.GetChild(0).GetChild(i + 1).GetChild(1).GetComponent<Text>().text;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = GameObject.Find("Inventory").transform.GetChild(0).GetChild(i + 1).GetChild(2).GetComponent<Text>().text;
                    transform.parent.parent.GetChild(k).GetChild(0).GetComponent<Image>().sprite = transform.parent.parent.GetChild(i + 1).GetChild(0).GetComponent<Image>().sprite;
    	}
	}
	}
	}*/
	}
}
