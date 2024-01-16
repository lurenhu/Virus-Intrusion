using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _maxHealth;
    [SerializeField] private int _currentHealth;

    private Player player;
    private Enemy enemy;

    private void Awake() {
        player = GetComponent<Player>();
        enemy = GetComponent<Enemy>();
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;
        }
        if (_currentHealth <= 0)
        {
            if (player != null)
            {
                player.PlayerDestroyed();
            }
            else if(enemy != null)
            {
                enemy.EnemyDestroyed();
            }
            else
            {
                Debug.Log("no binding");
            }
        }
    }

    public void HealDamage(int heal)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += heal;
        }
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }
}
