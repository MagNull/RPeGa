using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using AbilitySupports;
using Random = UnityEngine.Random;

public class DamageCalculator : MonoBehaviour
{
    [HideInInspector]
    public float CritMultiplier = 1;
    [HideInInspector]
    public float CritChance;
    
    [SerializeField] private float damageBonus = 0;

    public float DamageBonus
    {
        get => damageBonus;
        set => damageBonus = value;
    }

    public float CalculateDamage(float damage)
    {
        float resultDamage = damage + damageBonus;
        if (CritChance > 0)
        {
            CritTest(ref resultDamage);
        }
        return resultDamage;
    }

    private void CritTest(ref float resultDamage)
    {
        int roll = Random.Range(1, 101);
        if (roll <= CritChance) resultDamage *= CritMultiplier;
    }
}
