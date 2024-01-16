using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameResources : MonoBehaviour
{
    private static GameResources _instance;
    public static GameResources Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<GameResources>("Prefab/GameResources/GameResources");
            }
            return _instance;
        }
    }

    [Space(10)]
    [Header("LEVEL RESOURCE")]
    public List<LevelTemplateSO> LevelList;

    [Space(10)]
    [Header("PLAYER RESOURCE")]
    public List<PlayerSO> playerList;

    [Space(10)]
    [Header("ENEMY RESOURCE")]
    public List<EnemySO> enemyList;

    [Space(10)]
    [Header("TILEBASE")]
    public TileBase enemyUnWalkableTile;
    public TileBase enemyWalkableTile;
    public TileBase PlayerPlatformTile;
    public TileBase HasPlayerInPlatformTile;

    [Space(10)]
    [Header("MATERIAL")]
    public Material LitMaterial;




}
