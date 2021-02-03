using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 1;
    [SerializeField] private bool canDamage = false;

    public void ChangeDamageState()
    {
        canDamage = !canDamage;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (canDamage)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            { 
                damageable.TakeDamage(damage); 
            }  
        }
    }
}
