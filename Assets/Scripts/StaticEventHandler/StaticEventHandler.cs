using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticEventHandler
{
    public static event Action<LevelSpawnEventArgs> OnLevelSpawn;

    public static void CallLevelSpawnEvent(Level level)
    {
        OnLevelSpawn?.Invoke(new LevelSpawnEventArgs() { level = level });
    }
}

public class LevelSpawnEventArgs : EventArgs
{
    public Level level;
}
