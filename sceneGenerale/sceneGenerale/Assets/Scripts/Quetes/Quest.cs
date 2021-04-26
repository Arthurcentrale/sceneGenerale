using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Quest : MonoBehaviour
{
    enum Type { Collecte, Deplacement, Interaction};
    string Name;
    string Description;
    bool Completeted;

}

public class QuestList : Quest
{
    List<Quest> Quests;
    List<ItemAmount> Rewards;
}