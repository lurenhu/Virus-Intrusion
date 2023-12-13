using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovementAI : MonoBehaviour
{
    private Enemy enemy;
    private float moveSpeed;
    private Stack<Vector3> movementSteps = new Stack<Vector3>();
    private Coroutine moveEnemyRoutine;
    private WaitForFixedUpdate waitForFixedUpdate;


    private void Awake() {
        enemy = GetComponent<Enemy>();
    }

    private void Start() {

        moveSpeed = enemy.enemySOData.moveSpeed;

        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void Update() {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        CreatePath();

        if (movementSteps != null)
        {
            if (moveEnemyRoutine != null)
            {
                StopCoroutine(moveEnemyRoutine);
            }
            moveEnemyRoutine = StartCoroutine(MoveEnemyRoutine(movementSteps));
        }
    }

    private IEnumerator MoveEnemyRoutine(Stack<Vector3> movementSteps)
    {
        while (movementSteps.Count > 0)
        {
            Vector3 nextPosition = movementSteps.Pop();

            while (Vector3.Distance(nextPosition, transform.position) > 0.2f)
            {
                // Trigger movement event
                enemy.movementToPositionEvent.CallMovementToPositionEvent(nextPosition, transform.position, moveSpeed, (nextPosition - transform.position).normalized);

                yield return waitForFixedUpdate;  

            }

            yield return waitForFixedUpdate;
        }
    }

    private void CreatePath()
    {
        Level currentLevel = GameManager.Instance.GetCurrentLevel();
        if (currentLevel == null)
        {
            return;
        }

        Vector2Int[] enemyTargetPositions = currentLevel.targetPositionArray;

        Vector3Int enemyGridPosition = currentLevel.instantiateLevel.grid.WorldToCell(transform.position);

        movementSteps = Astar.BuildPath(currentLevel,enemyGridPosition,(Vector3Int)enemyTargetPositions[0]);

        if (movementSteps != null)
        {
            movementSteps.Pop();
        }else
        {

        }
    }
}
