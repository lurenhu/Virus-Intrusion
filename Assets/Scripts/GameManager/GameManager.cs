using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    public Transform Camera;

    protected override void Awake() {
        base.Awake();
        Camera = GameObject.FindWithTag("MainCamera").transform;
    }

    private void Start() {
        LevelSpawner.Instance.GenerateLevel("Tilemap_Level1");
    }

    private void Update() {
        
    }


    /// <summary>
    /// 通过uid获取对应关卡
    /// </summary>
    public LevelTemplateSO GetLevelByUid(string uid)
    {
        LevelTemplateSO levelTemplate = new LevelTemplateSO();
        foreach (LevelTemplateSO level in GameResources.Instance.LevelList)
        {
            if (level.uid == uid)
            {
                levelTemplate = level;
            }
        }
        return levelTemplate;
    }
}
