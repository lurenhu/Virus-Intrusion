using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_", menuName = "ScriptableObjects/Player_")]

public class PlayerSO : ScriptableObject
{
    [Space(10)]
    [Header("PLAYER DETAIL")]
    public string playerName;
    public GameObject playerPrefab;

    [Space(10)]
    [Header("PLAYER HEALTH DETAIL")]
    public int playerMaxHealth;
    public int playerAttack;

    [Space(10)]
    [Header("PLAYER ATTACK AREA")]
    public Vector2Int raw;
    public float playerAttackAreaRadius;
    
}
