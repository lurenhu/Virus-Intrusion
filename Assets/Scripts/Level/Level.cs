using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class Level
{
    [Space(10)]
    [Header("LEVEL PROPERTY")]
    public string name;
    public string templateId;
    public GameObject prefab;
    public Vector2Int lowerBound;
    public Vector2Int upperBound;

    [Space(10)]
    [Header("LEVEL ENEMY PROPERTY")]
    public List<LevelEnemyGenerateRule> levelEnemyGenerateRule;
    public Vector2Int[] spawnPositionArray;
    public Vector2Int[] targetPositionArray;
    public Vector2Int[] setupPositionArray;
    public InstantiateLevel instantiateLevel;

    
}
