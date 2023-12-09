using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private void Start() {
        EnemySO enemy = GameResources.Instance.enemyList[0];

        LevelTemplateSO level = GameResources.Instance.LevelList[0];

        Grid grid = level.prefab.transform.GetComponent<Grid>();

        Vector3 cellPosition = grid.CellToWorld((Vector3Int)level.spawnPositionArray[0]);
        Vector3Int position = new Vector3Int(-(level.upperBound.x + level.lowerBound.x)/2,-(level.upperBound.y + level.lowerBound.y)/2,0);
        Vector3 offset = new Vector3(0.5f,0.3f);
        Vector3 targetPosition = cellPosition + position + offset;

        GameObject enemyInWorld = Instantiate(enemy.prefab,targetPosition,Quaternion.identity,transform);
    }
}
