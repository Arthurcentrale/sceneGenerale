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

public class TalkingGameEvent : GameEvent
{
    public string HabitantName;

    public TalkingGameEvent(string name)
    {
        HabitantName = name;
    }
}

public class MairieGameEvent : GameEvent
{
    public string mairieName;
    public MairieGameEvent()
    {
        mairieName = "mairie";
    }
}

public class CraftingGameEvent : GameEvent
{
    public string craftName;
    
    public CraftingGameEvent(string name)
    {
        craftName = name;
    }
}
