using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGoal : Mission.MissionGoal
{
    public string Building;

    public override string GetDescription()
    {
        return $"Build a {Building}";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<BuildingGameEvent>(OnBuilding);
    }

    private void OnBuilding(BuildingGameEvent eventInfo)
    {

        if (eventInfo.BuildingName == Building)
        {
            CurrentAmount++;
            Evaluate();
        }
        
    }
}
