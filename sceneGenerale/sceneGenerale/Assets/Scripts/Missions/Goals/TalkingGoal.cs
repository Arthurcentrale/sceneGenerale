using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingGoal : Mission.MissionGoal
{
    public string habitant;

    public override string GetDescription()
    {
        return $"Parler à {habitant}";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<TalkingGameEvent>(OnTalking);
    }

    private void OnTalking(TalkingGameEvent eventInfo)
    {

        if (eventInfo.HabitantName == habitant)
        {
            CurrentAmount++;
            Evaluate();
        }

    }


}
