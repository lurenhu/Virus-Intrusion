using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerSO playerSO;

    public Transform shootPosition;
    private Transform targetPosition;
    [SerializeField] private CircleCollider2D playerAttackArea;
    [SerializeField] private CapsuleCollider2D playerHitArea;
    public Queue<GameObject> enemiesInArea = new Queue<GameObject>();
    private Health health;

    private void Awake() {
        health = GetComponent<Health>();
        playerSO = GameResources.Instance.playerList[0];
    }

    private void Start() {
        health.SetMaxHealth(playerSO.playerMaxHealth);
        playerAttackArea.radius = playerSO.playerAttackAreaRadius;
    }

    private void Update() {
        //HasEnemyInArea();
    }

    private void HasEnemyInArea()
    {
        if (enemiesInArea.Count == 0)
        {
            return;
        }

        targetPosition = enemiesInArea.Peek().transform;

        GameObject ammo = GenerateAmmo();

    }

    private GameObject GenerateAmmo()
    {
        GameObject ammoPrefab = GameResources.Instance.ammoList[0].ammoPrefab;
        GameObject ammo = Instantiate(ammoPrefab, shootPosition.position, Quaternion.identity);

        Ammo ammoComponent = ammo.GetComponent<Ammo>();
        ammoComponent.InitializeAmmo(GameResources.Instance.ammoList[0],targetPosition);

        return ammo;
    }
}
