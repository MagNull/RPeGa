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

    public override void Init(DamageDealer dd, IDamageable damageable)
    {
        base.Init(dd, damageable);
        _damageDealer.OnHitStart += CritTest;
        _damageDealer.OnHitEnd += RevertDamage;
    }

    private void CritTest()
    {
        int roll = Random.Range(0, 101);
        if (roll <= critChance) _damageDealer.damage *= critMultiplayer;
    }

    private void RevertDamage()
    {
        _damageDealer.damage = _damageDealer.PureDamage;
    }
}
