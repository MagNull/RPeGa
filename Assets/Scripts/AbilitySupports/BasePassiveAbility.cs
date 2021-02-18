using UnityEngine;
using UnityEngine.UI;
using WeaponScripts;

namespace AbilitySupports
{
    public abstract class BasePassiveAbility : ScriptableObject
    {
        protected DamageCalculator _damageCalculator;
        protected PlayerSpeedManipulator _manipulator;
        protected PlayerResources _playerResources;
        public void Init(DamageCalculator damageCalculator, PlayerSpeedManipulator manipulator, PlayerResources resources)
        {
            _damageCalculator = damageCalculator;
            _manipulator = manipulator;
            _playerResources = resources;
        }

        public abstract void ApplyEffect();
    }
}