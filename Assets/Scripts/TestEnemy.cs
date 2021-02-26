using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 10;
    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    public void OnDamageTaken(float damage)
    {
        
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died.");
    }
}
