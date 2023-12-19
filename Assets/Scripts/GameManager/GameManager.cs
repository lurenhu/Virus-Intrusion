using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [Space(10)]
    [Header("UI")]
    public int enemyTotalCount;
    public int enemyCurrentCount;
    public int PlayerHealth;

    public Transform Camera;

    private Level currentLevel;


    protected override void Awake() {
        base.Awake();
        Camera = GameObject.FindWithTag("MainCamera").transform;
    }

    private void Start() {
        GenerateLevelWithPanel("Tilemap_Level1");
        InitializeEnemyCount();
    }

    private void Update() {
        
    }
    
    private void GenerateLevelWithPanel(string levelName)
    {
        UIManager.Instance.OpenPanel(UIConst.LevelPanel);

        currentLevel = LevelSpawner.Instance.GenerateLevel(levelName);
        Camera.position = LevelSpawner.Instance.GetLevelCenterPositionInWorld(levelName);
    }

    private void InitializeEnemyCount()
    {
        enemyTotalCount = 0;
        foreach (LevelEnemyGenerateRule levelEnemyGenerateRule in currentLevel.levelEnemyGenerateRule)
        {
            enemyTotalCount += levelEnemyGenerateRule.enemyCount;
        }
        enemyCurrentCount = enemyTotalCount;

        PlayerHealth = 3;
    }

    public void CallEnemyDeath()
    {
        enemyCurrentCount--;
        PlayerHealth--;
    }

    /// <summary>
    /// 获取当前生成的关卡
    /// </summary>
    public Level GetCurrentLevel() 
    {
        return currentLevel;
    }


    
}
