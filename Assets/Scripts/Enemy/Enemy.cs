using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

[RequireComponent(typeof(MovementToPosition))]
[RequireComponent(typeof(MovementToPositionEvent))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public EnemySO enemySOData;
    private List<Vector2> targetPositions = new List<Vector2>();
    public bool hasInTarget = false;
    public MovementToPosition movementToPosition;
    public MovementToPositionEvent movementToPositionEvent;
    public Rigidbody2D rb2D;
    public Health health;

    private void Awake() {
        health = GetComponent<Health>();

        rb2D = GetComponent<Rigidbody2D>();

        movementToPosition = GetComponent<MovementToPosition>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
    }

    private void Start() {
    }

    private void Update() {
        CheckEnemyInTarget();

        
    }

    /// <summary>
    /// 实例化敌人数据
    /// </summary>
    /// <param name="enemySO">敌人数据</param>
    /// <param name="level">当前生成关卡的数据</param>
    public void InitializeEnemy(EnemySO enemySO,Level level,int enemyMaxHealth)
    {
        this.enemySOData = enemySO;

        health.SetMaxHealth(enemyMaxHealth);

        Vector2Int[] targetGridPosition = level.targetPositionArray;
        Grid grid = level.instantiateLevel.grid;
        foreach (Vector2Int target in targetGridPosition)
        {
            Vector2 targetPosition = grid.CellToWorld((Vector3Int)target);
            targetPositions.Add(targetPosition);
        }
    }

    /// <summary>
    /// 检查敌人是否已进入目标点
    /// </summary>
    private void CheckEnemyInTarget()
    {
        if (targetPositions.Count == 0)
        {
            return;
        }

        foreach (Vector2 targetPosition in targetPositions)
        {
            if (Vector2.Distance(transform.position, targetPosition) < 0.5f)
            {
                EnemyInTargetArea();
            }
        }
    }

    /// <summary>
    /// 敌人进入目标点
    /// </summary>
    private void EnemyInTargetArea()
    { 
        GameManager.Instance.CallEnemyDeath();
        Debug.Log("Call Enemy In Area");

        Destroy(gameObject);
    }

    /// <summary>
    /// 敌人死亡
    /// </summary>
    public void EnemyDestroyed()
    {
        DestroyedEvent destroyedEvent = GetComponent<DestroyedEvent>();
        destroyedEvent.CallDestroyedEvent(false);
    }
}
