using AbilitiesScripts;
using UnityEngine;
using Zenject;

namespace WeaponScripts
{
    public class Shield : Weapon
    {
        public float BaseDamage = 1;
        public FireShield FireShield;
        
        [Inject] 
        private DamageCalculator _damageCalculator;

        protected override void Awake()
        {
            base.Awake();
            FireShield = GetComponentInChildren<FireShield>();
        }

        protected override void DealDamage(IDamageable damageable, float damage)
        {
            float resultDamage = _damageCalculator.CalculateDamage(damage);
            damageable.TakeDamage(resultDamage);
        }

        public override void PlayMeleeAttackAnimation()
        {
            _animator.SetTrigger("Shield Bash");
        }

        public void OnCollisionEnter(Collision other)
        {
            if (_canDamage)
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    float damage = BaseDamage;
                    DealDamage(damageable, damage);
                }
            }
        }
    }
}
