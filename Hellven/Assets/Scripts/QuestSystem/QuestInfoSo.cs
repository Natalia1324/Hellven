using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    
    public string displayName;

    [Header("Requierements")]

    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]

    public int goldReward;
     
    private void onValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif    
    }
}
