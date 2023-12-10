using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Level
{
    public string name;
    public string templateId;
    public GameObject prefab;
    public Vector2Int lowerBound;
    public Vector2Int upperBound;
    public Vector2Int[] spawnPositionArray;
    public Vector2Int[] targetPositionArray;
    public Vector2Int[] setupPositionArray;
    public InstantiateLevel instantiateLevel;
    
}
