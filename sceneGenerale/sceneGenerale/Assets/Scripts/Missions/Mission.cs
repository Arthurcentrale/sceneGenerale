using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Mission : ScriptableObject
{
    public struct Info
    {
        public string Name;
        public Sprite Icon;
        public string Description;
    }

    [Header("Info")] public Info Information;

    [System.Serializable]
    public struct Stat
    {
        public int Currency;
        public int XP;
    }

    [Header("Reward")] public Stat Reward = new Stat { Currency = 10 , XP = 10};

    public bool Completed { get; protected set; }
    public MissionCompletedEvent MissionCompleted;

    public abstract class MissionGoal : ScriptableObject
    {
        protected string Description;
        public int CurrentAmount { get; protected set; }
        public int RequiredAmount = 1;

        public bool Completed { get; protected set; }
        [HideInInspector] public UnityEvent GoalCompleted;

        public virtual string GetDescription()
        {
            return Description;
        }

        public virtual void Initialize()
        {
            Completed = false;
            GoalCompleted = new UnityEvent();
        }

        protected void Evaluate()
        {
            if (CurrentAmount >= RequiredAmount)
            {
                Complete();
            }
        }

        private void Complete()
        {
            Completed = true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }

        

        
    }

    public List<MissionGoal> Goals;

    public void Initialize()
    {
        Completed = false;
        MissionCompleted = new MissionCompletedEvent();

        foreach (var goal in Goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    private void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
        if (Completed)
        {
            //donner récompense
            MissionCompleted.Invoke(this);
            MissionCompleted.RemoveAllListeners();
        }
    }
}
public class MissionCompletedEvent : UnityEvent<Mission> { }

#if UNITY_EDITOR
[CustomEditor(typeof(Mission))]
public class MissionEditor : Editor
{
    SerializedProperty m_MissionInfoProperty;
}

#endif