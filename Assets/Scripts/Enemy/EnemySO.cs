using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "ScriptableObjects/Enemy_")]
public class EnemySO : ScriptableObject
{
    [Space(10)]
    [Header("ENEMY DETAIL")]
    public string enemyName;
    public GameObject prefab;
    public float moveSpeed;
}
