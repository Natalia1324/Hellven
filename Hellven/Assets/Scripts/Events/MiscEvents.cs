using System;

public class MiscEvents
{
    public event Action onKilledMonsters;
    public void Killmonster()
    {
        if (onKilledMonsters != null)
        {
            onKilledMonsters();
        }
    }
}