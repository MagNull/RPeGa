using System;
using AbilitySupports;
using UnityEngine;
using WeaponScripts;
using Random = UnityEngine.Random;

namespace AbilitiesScripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Passive/Crit Chance")]
    public class CritChancePassive : PassiveBonus
    {
        [SerializeField] private float _critChance = 1;
        [SerializeField] private float _critMultiplayer = 2;

        public override void ApplyEffect()
        {
            _damageCalculator.CritChance = _critChance;
            _damageCalculator.CritMultiplier = _critMultiplayer;
        }

       
    }
}
