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

    public void Initialize(Mission mission)
    {
        titleText.text = mission.Information.Name;
        descriptionText.text = mission.Information.Description;

        foreach (var goal in mission.Goals)
        {
            GameObject goalObj = Instantiate(goalPrefab, goalsContent);
            goalObj.transform.Find("Text").GetComponent<Text>().text = goal.GetDescription();

            GameObject countObj = goalObj.transform.Find("Count").gameObject;
            
            if (goal.Completed)
            {
                countObj.SetActive(false);
                goalObj.transform.Find("Done").gameObject.SetActive(true);
            }

            else
            {
                countObj.GetComponent<Text>().text = goal.CurrentAmount + "/" + goal.RequiredAmount;
            }
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
}
