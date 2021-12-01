using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IUBatis : MonoBehaviour
{

    private GameObject panel;
    bool open;
    bool onPanel;
    public bool isOccupied;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;

    float timer = 0f;
    float delai = 5f;


    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("PanelBatisConstruction");

        onPanel = false;
        open = false;
        isOccupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickOnPanel()
    {
        onPanel = true;
        Deplacement.enMenu = true;
    }

    public void ClickOutPanel()
    {
        onPanel = false;
        Deplacement.enMenu = false;
    }
}
