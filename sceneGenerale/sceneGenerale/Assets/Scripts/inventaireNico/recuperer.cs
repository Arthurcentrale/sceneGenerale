using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class recuperer : MonoBehaviour
{   
    //public GameObject Inventory;



    Inventaire Inventaire_script;
    //public Sprite Vide, Berry_02, None, Berry_01;
    //GameObject P;

    void Start()
    {
        Inventaire_script = GameObject.Find("Inventory").GetComponent<Inventaire>();

        //P = transform.parent.gameObject;
    }

    // Start is called before the first frame update


    public void Recupere()
    {
        int i;
        int n=int.Parse(GameObject.Find("Inventory").transform.GetChild(0).GetChild(12).GetComponent<Text>().text); // nombre de slot occupe

        // Nr Slot
        //int nrSlot = transform.parent.GetSiblingIndex(); // concerne Geatan
        // Decremente



        //print(transform.parent.GetChild(1).GetComponent<Text>().text);

        //print(GameObject.Find("Inventory").transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text);



        //if (gameObject.GetComponent<Text>().text == "Baie2")
        /*
        print(transform.parent.GetChild(1).GetComponent<Text>().text == GameObject.Find("Inventory").transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text);
        print(transform.parent.GetChild(1).GetComponent<Text>().text.ToString() == "Baie");
        print(GameObject.Find("Inventory").transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text == "Baie");
        */
        
        int action = 0; // permer de savoir si l'objet a ete recupere

        for (i = 1; i < 11; i++)
        {
            
            //print(GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text);
            
            if ((transform.parent.GetChild(1).GetComponent<Text>().text == GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text) && (Inventaire_script.Slot[i].Amount < 5))
            //if (String.Compare(transform.parent.GetChild(1).GetComponent<Text>().text, GameObject.Find("Inventory").transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text) == 0)
            {
                Inventaire_script.Slot[i].Amount += 1;


                GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = Inventaire_script.Slot[i].Amount.ToString();
                action = 1;
            }
            
            /*
            if (GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text != "")
            {
            n+=1;
            }
            */
        }



        
        //print(n);

            
            if ((n < 10) && (action<1))
            {
                //print(GameObject.Find("Inventory").transform.GetChild(0).GetChild(n).GetChild(1).GetComponent<Text>().text);
                //print(transform.parent.GetChild(1).GetComponent<Text>().text);
                GameObject.Find("Inventory").transform.GetChild(0).GetChild(n).GetChild(1).GetComponent<Text>().text = transform.parent.GetChild(1).GetComponent<Text>().text;
                //print(GameObject.Find("Inventory").transform.GetChild(0).GetChild(n).GetChild(1).GetComponent<Text>().text);

                Inventaire_script.Slot[n].Amount += 1;
		//Inventaire_script.Slot[i].Item.id += 1;
		//Inventaire_script.Slot[n].Item.ItemName = transform.parent.GetChild(1).GetComponent<Text>().text;
		//Inventaire_script.Slot[n].Item.Icon = transform.parent.GetChild(0).GetComponent<Image>().sprite; 

                GameObject.Find("Inventory").transform.GetChild(0).GetChild(n).GetChild(2).GetComponent<Text>().text = Inventaire_script.Slot[n].Amount.ToString();
                //GameObject.Find("Inventory").transform.GetChild(0).GetChild(n).GetChild(O).GetComponent<Image>().sprite = Berry_02;
                GameObject.Find("Inventory").transform.GetChild(0).GetChild(n).GetChild(0).GetComponent<Image>().sprite = transform.parent.GetChild(0).GetComponent<Image>().sprite; 
            n++;
                //print(n);
                action = 1;
            }


        GameObject.Find("Inventory").transform.GetChild(0).GetChild(12).GetComponent<Text>().text = n.ToString();
        /*
        for (i = 0; i < 6; i++)
        {
          Inventaire_script.UpdateTXT2(i, Inventaire_script.Slot[i].ToString());
          Inventaire_script.UpdateTXT1(i, GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text);


        }*/

        //}
        /*
        if Inventaire_script.Slot[nrSlot] == 0{
            Inventaire_script.UpdateTXT(" ", Inventaire_script.Slot[nrSlot].ToString());




        }
        */
        /*
        for (i = 0; i < 6; i++)
        {
            Inventaire_script.UpdateTXT(1, Inventaire_script.Slot[i].ToString());
            Inventaire_script.UpdateTXT(2, Inventaire_script.Slot[i].ToString());
        }
        */
        //Inventaire_script.UpdateTXT(nrSlot,Inventaire_script.Slot[nrSlot].ToString());



        //Debug.Log(transform.parent.GetSiblingIndex());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
