using AbilitiesScripts;
using Other;
using UnityEngine;
using Zenject;

namespace WeaponScripts
{
    public class Shield : Weapon
    {
        public FireShield FireShield;
        
        [Inject] 
        private DamageCalculator _damageCalculator;

        protected void Awake()
        {
            FireShield = GetComponentInChildren<FireShield>();
        }

        protected override void DealDamage(IDamageable damageable, float damage)
        {
            float resultDamage = _damageCalculator.CalculateDamage(damage);
            damageable.TakeDamage(resultDamage);
        }
    }
}
