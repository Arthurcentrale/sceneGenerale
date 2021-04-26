using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using UnityStandardAsset.Character.FirstPerson;



public class Inventaire : MonoBehaviour
{

    bool activation = false;
    public GameObject Player;
    GameObject P;
    public Item Vide;
    public ItemAmount[] Slot;




    // Start is called before the first frame update
    void Start()
    {

        GetComponent<Canvas> ().enabled = false;
        P=transform.GetChild (0).gameObject;
        //Slot= new ItemAmount[P.transform.childCount];

    }

    // Update is called once per frame
    void Update()
    {
        updateinventaire();

        if (Input.GetKeyDown(KeyCode.I)){

        activation =!activation;

        // La partie suivante contrôle le deplacement du joueur (2options possibles) 

        //if (!activation){
            //Player.GetComponent<Rigidbody>(). enabled = true;
            //Player.GetComponent<RigidbodyFirstPersonController>(). enabled = true;

        //}
        //else {
            //Player.GetComponent<Rigidbody>(). enabled = false;
            //Player.GetComponent<RigidbodyFirstPersonController>(). enabled = false;
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        //}


 
        GetComponent<Canvas> ().enabled = activation;

        }
        

    }

    void updateinventaire()
    {
        int i = 0;
        foreach (ItemAmount ItemAmount in Slot)
        {
            if (ItemAmount.Item.ItemName != "Vide" && ItemAmount.Amount == 0)
            {
                Slot[i].Item = Vide;
            }
            i++;

            }
        
        for (i=1;i<11;i++){
            this.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().sprite=Slot[i].Item.Icon;
            this.transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text=Slot[i].Item.ItemName;
            this.transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text=Slot[i].Amount.ToString();
        }
    }

    public void UpdateTXT2 (int nsSlot, string txt)
    {
        if (txt != "0")
        {
            P.transform.GetChild(nsSlot).GetChild(2).GetComponent<Text>().text = txt;  //quantite en 3 eme position dans le slot
            
        }
        else
        {
            P.transform.GetChild(nsSlot).GetChild(2).GetComponent<Text>().text = " ";
            //print(txt);
        }
    }
    public void UpdateTXT1(int nsSlot, string txt)
    {
        //if (txt != "0")
        //{
        P.transform.GetChild(nsSlot).GetChild(1).GetComponent<Text>().text = txt;  //nom en 2 eme position dans le slot
        //}

    }

    public void UpdateN(int nsSlot, string txt)
    {
        //if (txt != "0")
        //{
        P.transform.GetChild(nsSlot).GetComponent<Text>().text = txt;  //nom en 2 eme position dans le slot
        //}

    }


}
