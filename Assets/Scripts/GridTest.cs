using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridTest : MonoBehaviour
{
    Grid grid;

    public Transform cellTarget;

    public Transform worldTarget;
    private void Awake() {
        grid = transform.GetComponent<Grid>();
    }

    private void Start() {
        Vector3 Positon  = grid.CellToWorld(new Vector3Int(1,1,0));
        Debug.Log(Positon);
        Vector3 Positon2 = grid.WorldToCell(worldTarget.position);
        Debug.Log(Positon2);
    }
}
