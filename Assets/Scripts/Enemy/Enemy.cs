using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO enemySOData;

    /// <summary>
    /// 实例化敌人数据
    /// </summary>
    /// <param name="enemySO">敌人数据</param>
    /// <param name="level">当前生成关卡的数据</param>
    public void InitializeEnemy(EnemySO enemySO,Level level)
    {
        this.enemySOData = enemySO;
    }
}
