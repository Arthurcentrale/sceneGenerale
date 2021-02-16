using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SelectionBox : MonoBehaviour
{
    public Canvas canvas;

    public Image selectionBox;

    private RectTransform rt;

    private Vector3 startScreenPos;

    public static bool finishedSelecting;
    public static Touch endPos;

    int waitTime;


    void Start()
    {
        canvas = this.gameObject.GetComponent<Canvas>();
        selectionBox = this.transform.GetChild(0).GetComponent<Image>();

        //We need to reset anchors and pivot to ensure proper positioning
        rt = selectionBox.GetComponent<RectTransform>();
        rt.pivot = Vector2.one * .5f;
        //rt.anchorMin = Vector2.one * .5f;
        //rt.anchorMax = Vector2.one * .5f;
        selectionBox.gameObject.SetActive(false);

        finishedSelecting = false;

        waitTime = 0;
    }

    void Update()
    {
        bool beginToSelect = Agriculture.beginToSelect;

        if ((beginToSelect) && (!DialogUI.panelTouched) && (!finishedSelecting))
        {
            startScreenPos = Agriculture.TouchToPos(Agriculture.farmCorner1);
            Debug.Log("startscreenpos : " + startScreenPos.ToString());
            waitTime++;

            if ((Player_script.VerifyTouch()) && (waitTime > 200))
            {
                Touch touch = Player_script.ImportTouch();

                if (touch.phase == TouchPhase.Ended)
                {
                    selectionBox.gameObject.SetActive(false);
                    finishedSelecting = true;
                    endPos = touch;
                }
                else
                {
                    Vector3 pos = Agriculture.TouchToPos(touch);

                    float x0 = MiseEchelleCanvas(startScreenPos.x, canvas);
                    float x1 = MiseEchelleCanvas(pos.x, canvas);
                    float y0 = MiseEchelleCanvas(-startScreenPos.z, canvas);
                    float y1 = MiseEchelleCanvas(-pos.z, canvas);

                    if (x0 <= x1)
                    {
                        if (y0 <= y1)
                        {
                            rt.anchorMin = new Vector2(x0, y0);
                            rt.anchorMax = new Vector2(x1, y1);
                        }
                        else
                        {
                            rt.anchorMin = new Vector2(x0, y1);
                            rt.anchorMax = new Vector2(x1, y0);
                        }
                        
                    }
                    else
                    {
                        if (y0 <= y1)
                        {
                            rt.anchorMin = new Vector2(x1, y0);
                            rt.anchorMax = new Vector2(x0, y1);
                        }
                        else
                        {
                            rt.anchorMin = new Vector2(x1, y1);
                            rt.anchorMax = new Vector2(x0, y0);
                        }
                    }

                    rt.sizeDelta = new Vector2(1f, 1f);
                    
                    selectionBox.gameObject.SetActive(true);
                    Debug.Log("Mise à jour sélection");
                }
            }
        }
    }

    float MiseEchelleCanvas(float x,Canvas c)
    {
        RectTransform r = canvas.GetComponent<RectTransform>();
        float taille = r.sizeDelta.x;
        //Debug.Log("taille : " + taille.ToString());
        return (x / taille);
    }
}
 