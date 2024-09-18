using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillMonsterStep : QuestStep
{
    private int KilledMonsters = 0;
    private int MonstersToComplete = 5;

    private void OnEnable()
    {
        if (GameEventsManager.instance != null && GameEventsManager.instance.miscEvents != null)
        {
            GameEventsManager.instance.miscEvents.onKilledMonsters += MonsterKilled;
        }
        else
        {
            Debug.LogError("GameEventsManager.instance or GameEventsManager.instance.miscEvents is null in OnEnable of KillMonsterStep.");
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null && GameEventsManager.instance.miscEvents != null)
        {
            GameEventsManager.instance.miscEvents.onKilledMonsters -= MonsterKilled;
        }
    }

    public void MonsterKilled()
    {
        if (KilledMonsters < MonstersToComplete)
        {
            KilledMonsters++;
        }

        if (KilledMonsters >= MonstersToComplete)
        {
            FinishQuestStep();
        }
    }
}
