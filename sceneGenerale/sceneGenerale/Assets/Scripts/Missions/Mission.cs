using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Mission : ScriptableObject
{
    [System.Serializable]
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
    SerializedProperty m_MissionStatProperty;

    List<string> m_MissionGoalType;
    SerializedProperty m_MissionGoalListProperty;

    [MenuItem("Assets/Mission", priority = 0)]
    public static void CreateMission()
    {
        var newMission = CreateInstance<Mission>();

        ProjectWindowUtil.CreateAsset(newMission, "mission.asset");
    }

    void OnEnable()
    {
        m_MissionInfoProperty = serializedObject.FindProperty(nameof(Mission.Information));
        m_MissionStatProperty = serializedObject.FindProperty(nameof(Mission.Reward));

        m_MissionGoalListProperty = serializedObject.FindProperty(nameof(Mission.Goals));

        var lookup = typeof(Mission.MissionGoal);
        m_MissionGoalType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))
            .Select(type => type.Name)
            .ToList();
    }

    public override void OnInspectorGUI()
    {
        var child = m_MissionInfoProperty.Copy();
        var depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Mission info", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);

        }

        child = m_MissionStatProperty.Copy();
        depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Mission Reward", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }

        int choice = EditorGUILayout.Popup("Add new Mission Goal", -1, m_MissionGoalType.ToArray());

        if (choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(m_MissionGoalType[choice]);

            AssetDatabase.AddObjectToAsset(newInstance, target);

            m_MissionGoalListProperty.InsertArrayElementAtIndex(m_MissionGoalListProperty.arraySize);
            m_MissionGoalListProperty.GetArrayElementAtIndex(m_MissionGoalListProperty.arraySize - 1).objectReferenceValue = newInstance;

        }

        Editor ed = null;
        int toDelete = -1;

        for (int i = 0; i < m_MissionGoalListProperty.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            var item = m_MissionGoalListProperty.GetArrayElementAtIndex(i);
            SerializedObject obj = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);

            ed.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();

        }

        if (toDelete != -1)
        {
            var item = m_MissionGoalListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item, true);

            m_MissionGoalListProperty.DeleteArrayElementAtIndex(toDelete);
            m_MissionGoalListProperty.DeleteArrayElementAtIndex(toDelete);

        }

        serializedObject.ApplyModifiedProperties();


    }
}

#endif