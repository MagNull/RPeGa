using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Passive/Crit Chance")]
public class CritChancePassive : BasePassiveAbility
{
    [SerializeField] private float critChance = 1;
    [SerializeField] private float critMultiplayer = 2;
    private int roll;

    public override void Init(DamageDealer mainHandWeapon, DamageDealer offHandWeapon, IDamageable damageable)
    {
        base.Init(mainHandWeapon, offHandWeapon, damageable);
        _mainHandWeapon.OnHitStart += CritTest;
        _mainHandWeapon.OnHitEnd += RevertDamage;
        _offHandWeapon.OnHitStart += CritTest;
        _offHandWeapon.OnHitEnd += RevertDamage;
        roll = 0;
    }

    private void CritTest()
    {
        roll = Random.Range(0, 101);
        if (roll <= critChance)
        {
            _mainHandWeapon.damage *= critMultiplayer;
            _offHandWeapon.damage *= critMultiplayer;
        }
    }

    private void RevertDamage()
    {
        if (roll <= critChance)
        {
            _mainHandWeapon.damage /= critMultiplayer;
            _offHandWeapon.damage /= critMultiplayer;
        }
    }
}
