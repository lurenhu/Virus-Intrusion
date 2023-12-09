using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "ScriptableObjects/Enemy_")]
public class EnemySO : ScriptableObject
{
    public GameObject prefab;
    public int health;
    public float moveSpeed;
    public float Attack;
}
