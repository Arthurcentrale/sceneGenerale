using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public string EventDescription;
}

public class BuildingGameEvent : GameEvent
{
    public string BuildingName;

    public BuildingGameEvent(string name)
    {
        BuildingName = name;
    }
}
