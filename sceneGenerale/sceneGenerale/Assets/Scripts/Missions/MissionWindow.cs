using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWindow : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private Transform goalsContent;
    [SerializeField] private Text xpText;
    [SerializeField] private Text coinsText;

    public GameObject pageDroite;
    public GameObject pageGauche;

    //[HideInInspector] public MissionManager missionManager;

    public void Initialize(Mission mission)
    {
        pageDroite.SetActive(true);
        titleText.text = mission.Information.Name;
        descriptionText.text = mission.Information.Description;

        Vector3 placement = new Vector3(1445, 520, 0);
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        int i = 0;
        CloseWindow();
        foreach (var goal in mission.Goals)
        {
            
            GameObject goalObj = Instantiate(goalPrefab, placement, rotation, goalsContent);
            goalObj.transform.Find("Text").GetComponent<Text>().text = goal.GetDescription();

            GameObject countObj = goalObj.transform.Find("Count").gameObject;
            
            if (goal.Completed)
            {
                GameObject.Find("Goal Manager").GetComponent<GoalManager>().pointsGoals += goal.points;
                Debug.Log(goal.points);
                goal.points = 0;
                Debug.Log(goal.points);
                countObj.SetActive(false);
                goalObj.transform.Find("Done").gameObject.SetActive(true);
            }

            else
            {
                countObj.GetComponent<Text>().text = goal.CurrentAmount + "/" + goal.RequiredAmount;
            }
            if (i > GameObject.Find("Goal Manager").GetComponent<GoalManager>().pointsGoals) goalObj.SetActive(false);
            placement.y -= 110;
            i++;
        }

        xpText.text = mission.Reward.XP.ToString();
        coinsText.text = mission.Reward.Currency.ToString();

        
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);

        for (int i = 0; i<goalsContent.childCount; i++)
        {
            Destroy(goalsContent.GetChild(i).gameObject);
        }
    }

    public void epingler()
    {
        
        if (!pageGauche.GetComponent<MissionManager>().epingléBool)
        {
            pageGauche.GetComponent<MissionManager>().missionEpinglee = GetComponent<MissionWindow>().titleText.text;
            pageGauche.GetComponent<MissionManager>().punaise.SetActive(true);
            pageGauche.GetComponent<MissionManager>().epingléBool = true;
            //pageGauche.GetComponent<MissionManager>().remplirMissionEpingle();
        }
        else
        {
            pageGauche.GetComponent<MissionManager>().missionEpinglee = "aucune";
            pageGauche.GetComponent<MissionManager>().punaise.SetActive(false);
            pageGauche.GetComponent<MissionManager>().epingléBool = false;
            pageGauche.GetComponent<MissionManager>().nettoyageRaccourciMission();
            pageGauche.GetComponent<MissionManager>().lieuMissionRaccourci.transform.GetChild(0).GetComponent<Text>().text = "Aucune mission épinglée";

        }

    }

    


}
