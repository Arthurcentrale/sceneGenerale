using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private GameObject missionPrefab;
    [SerializeField] private Transform missionsContent;
    [SerializeField] private GameObject missionHolder;

    public List<Mission> CurrentMissions;

    private void Awake()
    {
        Vector3 placement = new Vector3(-1450, 750, 0);
        Quaternion rotation = new Quaternion(0, 0, 0, 0);

        foreach(var mission in CurrentMissions)
        {
            mission.Initialize();
            mission.MissionCompleted.AddListener(OnMissionCompleted);

            GameObject missionObj = Instantiate(missionPrefab, placement, rotation, missionsContent);
            missionObj.transform.GetChild(0).GetComponent<Text>().text = mission.Information.Name;
            missionObj.transform.Find("Icon").GetComponent<Image>().sprite = mission.Information.Icon;

            missionObj.GetComponent<Button>().onClick.AddListener(delegate
            {
                missionHolder.GetComponent<MissionWindow>().Initialize(mission);
                missionHolder.SetActive(true);
            });
            placement.y -= 130;
        }
    }

    public void Build(string buildingName)
    {
        EventManager.Instance.QueueEvent(new BuildingGameEvent(buildingName));
    }

    private void OnMissionCompleted(Mission mission)
    {
        missionsContent.GetChild(CurrentMissions.IndexOf(mission)).Find("checkmark").gameObject.SetActive(true);
    }
}
