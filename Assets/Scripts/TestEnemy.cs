using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 10;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
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
