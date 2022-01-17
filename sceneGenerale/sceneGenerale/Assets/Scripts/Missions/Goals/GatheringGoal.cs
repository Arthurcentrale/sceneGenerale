using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringGoal : Mission.MissionGoal
{
    public string Item;
    public int Amount;

    public override string GetDescription()
    {
        return "Obtenir " + Amount +" "+ Item;
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<GatheringGameEvent>(OnGathering);
    }

    private void OnGathering(GatheringGameEvent eventInfo)
    {

        if (eventInfo.itemName == Item && eventInfo.amountName == Amount)
        {
            CurrentAmount++;
            Evaluate();
        }

    }
}
