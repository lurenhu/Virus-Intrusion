using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InstantiateLevel : MonoBehaviour
{
    public Level level;
    public Grid grid;
    public Tilemap Background_Tilemap;
    public Tilemap Ground_Tilemap;
    public Tilemap Front_Tilemap;
    public Tilemap Decoration_Tilemap;
    public Tilemap Collision_Tilemap;

    public void Initialize(GameObject levelGameObject)
    {
        PopulateTilemapMemberVariables(levelGameObject);
        DisableCollisionTilemapRenderer();
        InitializeLevelName(levelGameObject);
    }

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

    private void DisableCollisionTilemapRenderer()
    {
        Collision_Tilemap.transform.GetComponent<TilemapRenderer>().enabled = false;
    }

    private void InitializeLevelName(GameObject levelGameObject)
    {
        name = levelGameObject.name;
    }
    
}
