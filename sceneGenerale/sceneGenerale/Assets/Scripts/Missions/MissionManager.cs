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
    public List<Mission> IncomingMissions;

    new public GameObject camera;
    public GameObject player;

    public int totalCurrency = 0;
    private int gap = 65;

    private Vector3 placement = new Vector3(- 825, 468, 0);
    private Quaternion rotation = new Quaternion(0, 0, 0, 0);

    private void Awake()
    {
        
        foreach(var mission in CurrentMissions)
        {
            updateMissionsWindow(mission);
        }
    }

    public void Build(string buildingName)
    {
        EventManager.Instance.QueueEvent(new BuildingGameEvent(buildingName));
    }

    public void Talk(string habitantName)
    {
        EventManager.Instance.QueueEvent(new TalkingGameEvent(habitantName));
    }

    public void RenovMairie()
    {
        EventManager.Instance.QueueEvent(new MairieGameEvent());
    }

    public void Craft(string craftName)
    {
        EventManager.Instance.QueueEvent(new CraftingGameEvent(craftName));
    }

    public void Gather(string itemName)
    {
        int amount = player.GetComponent<Player>().uiInventory.CountItem(itemName);
        Debug.Log("Qté bois : " + amount);
        EventManager.Instance.QueueEvent(new GatheringGameEvent(itemName, amount));
    }

    private void OnMissionCompleted(Mission mission)
    {
        missionsContent.GetChild(CurrentMissions.IndexOf(mission)).Find("checkmark").gameObject.SetActive(true);
        totalCurrency += mission.Reward.Currency;
        manageMissions();
    }

    private void manageMissions()
    {
        if (totalCurrency == 10)
        {
            CurrentMissions.Add(IncomingMissions[0]);
            updateMissionsWindow(IncomingMissions[0]);
            camera.GetComponent<ConstructionDebloquage>().majConstru(0);
            camera.GetComponent<ConstructionDebloquage>().majConstru(1);
        }
        else if (totalCurrency == 20)
        {
            CurrentMissions.Add(IncomingMissions[1]);
            updateMissionsWindow(IncomingMissions[1]);
            camera.GetComponent<ConstructionDebloquage>().majConstru(2);
        }
        else if (totalCurrency == 30)
        {
            CurrentMissions.Add(IncomingMissions[2]);
            updateMissionsWindow(IncomingMissions[2]);
            for (int i =3; i < 10; i++)
            {
                camera.GetComponent<ConstructionDebloquage>().majConstru(i);
            }
            
        }
        else if (totalCurrency == 40)
        {
            CurrentMissions.Add(IncomingMissions[3]);
            updateMissionsWindow(IncomingMissions[3]);
        }
        else if (totalCurrency == 50)
        {
            CurrentMissions.Add(IncomingMissions[4]);
            updateMissionsWindow(IncomingMissions[4]);
        }
    }

    private void updateMissionsWindow(Mission mission)
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
        placement.y -= gap;
    }
}
