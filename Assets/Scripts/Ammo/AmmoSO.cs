using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo_", menuName = "ScriptableObjects/Ammo_")]
public class AmmoSO : ScriptableObject  
{
    [Space(10)]
    [Header("AMMO DETAIL")]
    public GameObject ammoPrefab;

    public float ammoSpeed;

    public int ammoDamage;

}
