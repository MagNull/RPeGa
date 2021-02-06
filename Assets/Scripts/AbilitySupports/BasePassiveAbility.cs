using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class BasePassiveAbility : ScriptableObject
{
    [SerializeField] protected Image abilityImage;
    protected DamageDealer _mainHandWeapon;
    protected DamageDealer _offHandWeapon;
    protected IDamageable _damageable;

    public virtual void Init(DamageDealer mainHandWeapon, DamageDealer offHandWeapon, IDamageable damageable)
    {
        _mainHandWeapon = mainHandWeapon;
        _offHandWeapon = offHandWeapon;
        _damageable = damageable;
    }



}