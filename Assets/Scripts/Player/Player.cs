using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Player : MonoBehaviour
{
    private PlayerSO playerSO;

    public Transform shootPosition;
    private Transform targetPosition;
    [SerializeField] private CircleCollider2D playerAttackArea;
    [SerializeField] private CapsuleCollider2D playerHitArea;
    public Queue<GameObject> enemiesInArea = new Queue<GameObject>();
    private Health health;
    private float attackCoolDown = 0;

    private void Awake() {
        health = GetComponent<Health>();
    }

    private void Start() {
        health.SetMaxHealth(playerSO.playerMaxHealth);
        playerAttackArea.radius = playerSO.playerAttackAreaRadius;
    }

    private void Update() {
        attackCoolDown += Time.deltaTime;

        if (attackCoolDown >= playerSO.playerAttackInterval)
        {
            attackCoolDown = 0;
            HasEnemyInArea();
        }
    }

    private void HasEnemyInArea()
    {
        if (enemiesInArea.Count == 0)
        {
            return;
        }

        targetPosition = enemiesInArea.Peek().transform;

        GenerateAmmo();

    }

    /// <summary>
    /// 生成子弹
    /// </summary>
    private void GenerateAmmo()
    {
        GameObject ammoGameObject = Instantiate(playerSO.ammoSO.ammoPrefab,shootPosition.position,Quaternion.identity);

        Ammo ammo = ammoGameObject.GetComponent<Ammo>();
        ammo.InitializeAmmo(playerSO.ammoSO,targetPosition);
    }

    /// <summary>
    /// 导入干员数据
    /// </summary>
    /// <param name="playerSO"></param>
    public void InitializePlayer(PlayerSO playerSO)
    {
        this.playerSO = playerSO;
    }

    /// <summary>
    /// 干员阵亡
    /// </summary>
    public void PlayerDestroyed()
    {
        DestroyedEvent destroyedEvent = GetComponent<DestroyedEvent>();
        destroyedEvent.CallDestroyedEvent(true);
    }
}
