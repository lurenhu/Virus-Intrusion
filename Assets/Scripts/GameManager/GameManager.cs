using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    public Transform Camera;

    private Level currentLevel;


    protected override void Awake() {
        base.Awake();
        Camera = GameObject.FindWithTag("MainCamera").transform;
    }

    private void Start() {
        currentLevel = LevelSpawner.Instance.GenerateLevel("Tilemap_Level1");
        Camera.position = LevelSpawner.Instance.GetLevelCenterPositionInWorld("Tilemap_Level1");
    }

    private void Update() {
        
    }

    /// <summary>
    /// 获取当前生成的关卡
    /// </summary>
    public Level GetCurrentLevel() 
    {
        return currentLevel;
    }


    
}
