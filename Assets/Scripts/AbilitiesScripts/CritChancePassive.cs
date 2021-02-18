using System;
using AbilitySupports;
using UnityEngine;
using WeaponScripts;
using Random = UnityEngine.Random;

namespace AbilitiesScripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Passive/Crit Chance")]
    public class CritChancePassive : BasePassiveAbility
    {
        [SerializeField] private float critChance = 1;
        [SerializeField] private float critMultiplayer = 2;

        public override void ApplyEffect()
        {
            _damageCalculator.CritChance = critChance;
            _damageCalculator.CritMultiplier = critMultiplayer;
        }

       
    }
}
