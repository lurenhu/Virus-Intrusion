using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_",menuName = "ScriptableObjects/Level_")]
public class LevelTemplateSO : ScriptableObject
{

    [Space(10)]
    [Header("LEVEL PROPERTY")]
    [HideInInspector]public string uid;
    public GameObject prefab;//关卡预制体
    [HideInInspector]public GameObject previousPrefab;//确保生成了uid
    public Vector2Int lowerBound;//下界
    public Vector2Int upperBound;//上界
    public Vector2Int[] spawnPositionArray;//怪物出生点
    public Vector2Int[] targetPositionArray;//怪物目标点
    public Vector2Int[] setupPositionArray;//可部署点



#if UNITY_EDITOR
    private void OnValidate() {
        if (uid == "" || previousPrefab != prefab)
        {
            uid = GUID.Generate().ToString();
            previousPrefab = prefab;
            EditorUtility.SetDirty(this);
        }
    }    
#endif
}
