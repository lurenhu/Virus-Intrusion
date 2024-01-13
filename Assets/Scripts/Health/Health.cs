using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _maxHealth;
    [SerializeField] private int _currentHealth;

    private Enemy enemy;

    private void Start() {

        enemy = GetComponent<Enemy>();
    }

    // public int MaxHealth
    // {
    //     get { return _maxHealth; }
    //     set { _maxHealth = value; }
    // }
    // public int CurrentHealth
    // {
    //     get { return _currentHealth; }
    //     set { _currentHealth = value; }
    // }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            //角色死亡
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }
}
