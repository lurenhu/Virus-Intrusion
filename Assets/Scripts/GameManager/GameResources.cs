using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources _instance;
    public static GameResources Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<GameResources>("Prefab/GameResources/GameResources");
            }
            return _instance;
        }
    }

    [Space(10)]
    [Header("LEVEL RESOURCE")]
    public List<LevelTemplateSO> LevelList;

    [Space(10)]
    [Header("ENEMY RESOURCE")]
    public List<EnemySO> enemyList;
}
