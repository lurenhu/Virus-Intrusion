using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_",menuName = "ScriptableObjects/Level_")]
public class LevelTemplateSO : ScriptableObject
{
    [HideInInspector]public string uid;

    [Space(10)]
    [Header("LEVEL PROPERTY")]
    public GameObject prefab;//关卡预制体
    [HideInInspector]public GameObject previousPrefab;//确保生成了uid
    public Vector2Int lowerBound;//下界
    public Vector2Int upperBound;//上界

    [Space(10)]
    [Header("ENEMY GENERATE IN LEVEL")]
    public List<LevelEnemyGenerateRule> levelEnemyGenerateRule;//怪物生成规律
    public Vector2Int[] spawnPositionArray;//怪物出生点
    public Vector2Int[] targetPositionArray;//怪物目标点

    




#if UNITY_EDITOR
    private void OnValidate() {
        if (uid == "" || previousPrefab != prefab)
        {
            uid = GUID.Generate().ToString();
            previousPrefab = prefab;
            EditorUtility.SetDirty(this);
        }
    }    
#endif
}

[System.Serializable]
public class LevelEnemyGenerateRule
{
    public EnemySO enemySO;//敌人类型
    public int enemyCount;//敌人数量
    public float enemySpawnInterval;
    public float beforeEnemySpawnTime;
}
