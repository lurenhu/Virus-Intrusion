using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarTest : MonoBehaviour
{
    private LevelTemplateSO level;
    private Grid grid;
    private Tilemap frontTilemap;
    private Tilemap pathTilemap;
    private Vector3Int startGridPosition;
    private Vector3Int endGridPosition;
    private TileBase startPathTile;
    private TileBase finishPathTile;

    private Vector3Int noValue = new Vector3Int(9999, 9999, 9999);
    private Stack<Vector3> pathStack;

    

    // Update is called once per frame
    private void Update()
    {
        if (level == null || startPathTile == null || finishPathTile == null || grid == null || pathTilemap == null) return;

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
        if (position.x < level.lowerBound.x || position.x > level.upperBound.x
            || position.y < level.lowerBound.y || position.y > level.upperBound.y)
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

        pathStack = Astar.BuildPath(level, startGridPosition, endGridPosition);

        if (pathStack == null) return;

        foreach (Vector3 worldPosition in pathStack)
        {
            pathTilemap.SetTile(grid.WorldToCell(worldPosition), startPathTile);
        }
    }

}
