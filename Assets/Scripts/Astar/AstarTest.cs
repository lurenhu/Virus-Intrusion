using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AstarTest : MonoBehaviour
{
    public InstantiateLevel instantiateLevel;
    public Grid grid;
    public Tilemap frontTilemap;
    public Tilemap pathTilemap;
    public Vector3Int startGridPosition;
    public Vector3Int endGridPosition;
    public TileBase startPathTile;
    public TileBase finishPathTile;

    private Vector3Int noValue = new Vector3Int(9999, 9999, 9999);
    private Stack<Vector3> pathStack;

    private void Awake() {
        startPathTile = GameResources.Instance.enemyWalkableTile;
        finishPathTile = GameResources.Instance.enemyUnWalkableTile;
    }

    private void OnEnable() {
        StaticEventHandler.OnLevelSpawn += StaticEventHandler_OnLevelSpawn;
    }

    private void OnDisable() {
        StaticEventHandler.OnLevelSpawn -= StaticEventHandler_OnLevelSpawn;
    }

    public void StaticEventHandler_OnLevelSpawn(LevelSpawnEventArgs levelSpawnEventArgs)
    {
        instantiateLevel = levelSpawnEventArgs.level.instantiateLevel;
        grid = instantiateLevel.transform.GetComponent<Grid>();
        frontTilemap = instantiateLevel.transform.Find("Tilemap_Front").GetComponent<Tilemap>();
        pathTilemap = null;
        startGridPosition = noValue;
        endGridPosition = noValue;

        SetPathTilemap();
    }

    private void SetPathTilemap()
    {
        Transform tilemapCloneTransform = instantiateLevel.transform.Find("Tilemap_Front(Clone)");

        // If the front tilemap hasn't been cloned then clone it
        if (tilemapCloneTransform == null)
        {
            pathTilemap = Instantiate(frontTilemap, grid.transform);
            pathTilemap.GetComponent<TilemapRenderer>().sortingOrder = 2;
            pathTilemap.GetComponent<TilemapRenderer>().material = GameResources.Instance.LitMaterial;
            pathTilemap.gameObject.tag = "Untagged";
        }
        // else use it
        else
        {
            pathTilemap = instantiateLevel.transform.Find("Tilemap_Front(Clone)").GetComponent<Tilemap>();
            pathTilemap.ClearAllTiles();
        } 
    }

    // Update is called once per frame
    private void Update()
    {
        if (instantiateLevel == null || startPathTile == null || finishPathTile == null || grid == null || pathTilemap == null) return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            ClearPath();
            SetStartPosition();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ClearPath();
            SetEndPosition();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DisplayPath();
        }
    }


    /// <summary>
    /// Set the start position and the start tile on the front tilemap
    /// </summary>
    private void SetStartPosition()
    {
        if (startGridPosition == noValue)
        {
            startGridPosition = grid.WorldToCell(HelperUtilities.GetMouseWorldPosition());
            Debug.Log("startGridPosition is " + startGridPosition);

            if (!IsPositionWithinBounds(startGridPosition))
            {
                startGridPosition = noValue;
                return;
            }

            pathTilemap.SetTile(startGridPosition, startPathTile);
        }
        else
        {
            pathTilemap.SetTile(startGridPosition, null);
            startGridPosition = noValue;
        }
    }


    /// <summary>
    /// Set the end position and the end tile on the front tilemap
    /// </summary>
    private void SetEndPosition()
    {
        if (endGridPosition == noValue)
        {
            endGridPosition = grid.WorldToCell(HelperUtilities.GetMouseWorldPosition());
            Debug.Log("endGridPosition is " + endGridPosition);

            if (!IsPositionWithinBounds(endGridPosition))
            {
                endGridPosition = noValue;
                return;
            }

            pathTilemap.SetTile(endGridPosition, finishPathTile);
        }
        else
        {
            pathTilemap.SetTile(endGridPosition, null);
            endGridPosition = noValue;
        }
    }


    /// <summary>
    /// Check if the position is within the lower and upper bounds of the room
    /// </summary>
    private bool IsPositionWithinBounds(Vector3Int position)
    {
        // If  position is beyond grid then return false
        if (position.x < instantiateLevel.level.lowerBound.x || position.x > instantiateLevel.level.upperBound.x
            || position.y < instantiateLevel.level.lowerBound.y || position.y > instantiateLevel.level.upperBound.y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    /// <summary>
    /// Clear the path and reset the start and finish positions
    /// </summary>
    private void ClearPath()
    {
        // Clear Path
        if (pathStack == null) return;

        foreach (Vector3 worldPosition in pathStack)
        {
            pathTilemap.SetTile(grid.WorldToCell(worldPosition), null);
        }

        pathStack = null;

        //Clear Start and Finish Squares
        endGridPosition = noValue;
        startGridPosition = noValue;
    }

    /// <summary>
    /// Build and display the AStar path between the start and finish positions
    /// </summary>
    private void DisplayPath()
    {
        if (startGridPosition == noValue || endGridPosition == noValue) return;

        pathStack = Astar.BuildPath(instantiateLevel.level, startGridPosition, endGridPosition);

        if (pathStack == null) return;

        foreach (Vector3 worldPosition in pathStack)
        {
            pathTilemap.SetTile(grid.WorldToCell(worldPosition), startPathTile);
        }
    }

}
