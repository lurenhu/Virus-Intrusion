using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    public Transform Camera;

    public GameObject test;
    protected override void Awake() {
        base.Awake();
        Camera = GameObject.FindWithTag("MainCamera").transform;
    }

    private void Start() {
        LevelSpawner.Instance.GenerateLevel("Tilemap_Level2");
        Camera.position = LevelSpawner.Instance.GetLevelCenterPositionInWorld("Tilemap_Level2");
    }

    private void Update() {
        
    }


    
}
