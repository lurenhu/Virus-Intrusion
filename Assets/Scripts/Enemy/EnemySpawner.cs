using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemySpawner : SingletonMonobehaviour<EnemySpawner>
{
    private List<LevelEnemyGenerateRule> levelEnemyGenerateRule;
    private Grid grid;
    private int enemyCount;
    private GameObject enemyPrefab;

    private void OnEnable() {
        StaticEventHandler.OnLevelSpawn += StaticEventHandler_OnLevelSpawn;
    }

    private void OnDisable() {
        StaticEventHandler.OnLevelSpawn -= StaticEventHandler_OnLevelSpawn;
    }

    private void StaticEventHandler_OnLevelSpawn(LevelSpawnEventArgs levelSpawnEventArgs)
    {
        Level level = levelSpawnEventArgs.level;

        this.grid = level.instantiateLevel.grid;
        this.levelEnemyGenerateRule = level.levelEnemyGenerateRule;

        CreateEnemyByLevel(level);
    }

    /// <summary>
    /// 通过所在关卡创建敌人
    /// </summary>
    private void CreateEnemyByLevel(Level level)
    {
        foreach (LevelEnemyGenerateRule generateRule in levelEnemyGenerateRule)
        {
            EnemySO enemySO = generateRule.enemySO;

            enemyCount = generateRule.enemyCount;
            enemyPrefab = enemySO.prefab;
            
            StartCoroutine(InitializeEnemy(level,enemyCount,enemyPrefab,enemySO,generateRule.enemySpawnInterval,generateRule.beforeEnemySpawnTime));
        }
    }


    /// <summary>
    /// 将敌人实力化至世界坐标
    /// </summary>
    private IEnumerator InitializeEnemy(Level level, int enemyCount, GameObject enemyPrefab,EnemySO enemySO, float enemySpawnInterval, float beforeEnemySpawnTime)
    {
        yield return new WaitForSeconds(beforeEnemySpawnTime);

        Sprite enemySprite = enemyPrefab.transform.GetComponent<SpriteRenderer>().sprite;
        Vector3 offset = enemySprite.pivot/new Vector2(enemySprite.rect.width,enemySprite.rect.height);

        foreach (Vector2Int spawnPoint in level.spawnPositionArray)
        {
            Vector3 spawnPosition = grid.CellToWorld((Vector3Int)spawnPoint) + offset;
        
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemyGameObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity,transform);

                Enemy enemyScript = enemyGameObject.GetComponent<Enemy>();
                enemyScript.InitializeEnemy(enemySO,level);
                
                yield return new WaitForSeconds(enemySpawnInterval);
            }
        }
    }
}
