using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingGoal : Mission.MissionGoal
{
    public string Craft;

    public override string GetDescription()
    {
        return $"Fabrique 1 {Craft}";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<CraftingGameEvent>(OnCrafting);
    }

    private void OnCrafting(CraftingGameEvent eventInfo)
    {

        if (eventInfo.craftName == Craft)
        {
            CurrentAmount++;
            Evaluate();
        }

    }
}
