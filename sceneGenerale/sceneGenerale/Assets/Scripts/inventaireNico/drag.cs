using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class drag : MonoBehaviour
{
    public GameObject correctForm;
    private bool moving;

    private float StartPosiX;
    private float StartPosiY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    public void OnMouseDown()
    {
        //print(2);
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            StartPosiX = mousePos.x - transform.parent.localPosition.x;
            StartPosiY = mousePos.y - transform.parent.localPosition.y;

            moving = true;

        }


    }

    public void OnMouseUp()
    {
        moving = false;
        //print(5);
    }

    void Update()
    {
        //print(moving);
        //print(Input.mousePosition);
        //print(Input.GetMouseButtonDown(0));        
        //print(transform.parent.localPosition); // atention position locale dans le slot, pas dans le panel !!!


        if (moving)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            //print(3);
            //print(mousePos);

            transform.parent.localPosition = new Vector3(mousePos.x - StartPosiX, mousePos.y - StartPosiY, transform.parent.localPosition.z);
        }
    }

}