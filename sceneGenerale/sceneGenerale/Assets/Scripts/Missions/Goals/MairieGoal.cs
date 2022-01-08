using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MairieGoal : Mission.MissionGoal
{
    public override string GetDescription()
    {
        return $"Rénover la mairie";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<MairieGameEvent>(OnMairie);
    }

    private void OnMairie(MairieGameEvent eventInfo)
    {
        if (eventInfo.mairieName == "mairie")
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}
