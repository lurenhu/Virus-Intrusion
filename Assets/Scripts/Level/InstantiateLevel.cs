using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InstantiateLevel : MonoBehaviour
{
    [HideInInspector] public Level level;
    [HideInInspector] public Grid grid;
    [HideInInspector] public Tilemap Background_Tilemap;
    [HideInInspector] public Tilemap Ground_Tilemap;
    [HideInInspector] public Tilemap Front_Tilemap;
    [HideInInspector] public Tilemap Decoration_Tilemap;
    [HideInInspector] public Tilemap Collision_Tilemap;
    [HideInInspector] public int[,] aStarMovementPenalty;

    public void Initialize(GameObject levelGameObject)
    {
        PopulateTilemapMemberVariables(levelGameObject);
        DisableCollisionTilemapRenderer();
        AddObstaclesAndPreferredPaths();
        InitializeLevelName(levelGameObject);
    }

    /// <summary>
    /// 填充Tilemap成员变量
    /// </summary>
    /// <param name="levelGameObject"></param>
    private void PopulateTilemapMemberVariables(GameObject levelGameObject)
    {
        grid = levelGameObject.GetComponent<Grid>();

        Tilemap[] tilemaps = levelGameObject.GetComponentsInChildren<Tilemap>();
        foreach (Tilemap tilemap in tilemaps)
        {
            if (tilemap.transform.tag == "Tilemap_BackGround")
            {
                Background_Tilemap = tilemap;
            }
            if (tilemap.transform.tag == "Tilemap_Ground")
            {
                Ground_Tilemap = tilemap;
            }
            if (tilemap.transform.tag == "Tilemap_Front")
            {
                Front_Tilemap = tilemap;
            }
            if (tilemap.transform.tag == "Tilemap_Decoration")
            {
                Decoration_Tilemap = tilemap;
            }
            if (tilemap.transform.tag == "Tilemap_Collision")
            {
                Collision_Tilemap = tilemap;
            }
        }
    }

    /// <summary>
    /// 将CollisionTilemap中的TilemapRenderer组件关闭
    /// </summary>
    private void DisableCollisionTilemapRenderer()
    {
        Collision_Tilemap.transform.GetComponent<TilemapRenderer>().enabled = false;
    }

    /// <summary>
    /// 赋值名字
    /// </summary>
    private void InitializeLevelName(GameObject levelGameObject)
    {
        name = levelGameObject.name;
    }

    /// <summary>
    /// 填充AStarMovementPenalty中的参数
    /// </summary>
    private void AddObstaclesAndPreferredPaths()
    {
        aStarMovementPenalty = new int[level.upperBound.x - level.lowerBound.x + 1, level.upperBound.y - level.lowerBound.y + 1];
        for (int i = 0; i < level.upperBound.x - level.lowerBound.x + 1; i++)
        {
            for (int j = 0; j < level.upperBound.y - level.lowerBound.y + 1; j++)
            {
                aStarMovementPenalty[i,j] = 40;

                TileBase tile = Collision_Tilemap.GetTile(new Vector3Int(level.lowerBound.x + i, level.lowerBound.y + j,0));

                if (tile == GameResources.Instance.enemyUnWalkableTile)
                {
                    aStarMovementPenalty[i,j] = 0;
                }
                if (tile == GameResources.Instance.enemyWalkableTile)
                {
                    aStarMovementPenalty[i,j] = 1;
                }
            }
        }
    }
    
}
