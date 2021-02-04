using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class BasePassiveAbility : ScriptableObject
{
    [SerializeField] protected Image abilityImage;
    protected DamageDealer _damageDealer;
    protected IDamageable _damageable;

    public virtual void Init(DamageDealer dd, IDamageable damageable)
    {
        _damageDealer = dd;
        _damageable = damageable;
    }



}