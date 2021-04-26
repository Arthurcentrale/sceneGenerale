using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Dialog : MonoBehaviour
{
    public static GameObject backgroundPanel;
    static GameObject DialogPanel;

    void Start()
    {
        backgroundPanel = this.transform.GetChild(0).gameObject;
        DialogPanel = backgroundPanel.transform.GetChild(0).gameObject;
    }

    public static void OpenDialogPanel()
    {
        DialogPanel.SetActive(true);
        backgroundPanel.SetActive(true);
    }

    public void CloseDialogPanel()
    {
        DialogPanel.SetActive(false);
        backgroundPanel.SetActive(false);
    }
}