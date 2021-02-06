using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damage = 1;
    public Action OnHitStart;
    public Action OnHitEnd;
    [SerializeField] private bool canDamage = false;
    private float _pureDamage;
    private ParticleSystem _particles;
    private float _damageBuffer;

    public float PureDamage
    {
        get => _pureDamage;
        set
        {
            damage = value;
            _damageBuffer = _pureDamage;
            _pureDamage = value;
        }
    }

    public void BufferizeDamage() => _damageBuffer = _pureDamage;

    private void Awake()
    {
        _pureDamage = damage;
        _particles = GetComponentInChildren<ParticleSystem>();
        if(_particles)_particles.enableEmission = false;
    }

    public void ChangeDamageState()
    {
        canDamage = !canDamage;
        if(_particles)_particles.enableEmission = !_particles.emission.enabled;
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (canDamage)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                if(OnHitStart != null)
                {
                    OnHitStart();
                }
                damageable.TakeDamage(damage);
                if(OnHitEnd != null)
                {
                    OnHitEnd();
                }
            }  
        }
    }
}
