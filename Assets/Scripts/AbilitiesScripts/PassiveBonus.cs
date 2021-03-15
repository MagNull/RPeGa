using AbilitySupports;
using Other;
using UnityEngine;

namespace AbilitiesScripts
{
    public abstract class PassiveBonus : ScriptableObject
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