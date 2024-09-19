using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private int globalKillCount = 0; 
    [SerializeField] private int killsPerLevel = 5; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void IncrementKillCount()
    {
        globalKillCount++;
    }

    public int GetGlobalEnemyLevel()
    {
        return Mathf.CeilToInt((globalKillCount / killsPerLevel) * UnityEngine.Random.Range(1f, 2f));
    }

    public void ResetKillCount()
    {
        globalKillCount = 0;
    }

    public int GetGlobalKillCount()
    {
        return globalKillCount;
    }
}
