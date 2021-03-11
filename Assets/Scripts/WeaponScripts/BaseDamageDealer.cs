using System;
using UnityEngine;

namespace WeaponScripts
{
    public class BaseDamageDealer : Weapon
    {
        public float BaseDamage = 1;

        protected override void DealDamage(IDamageable damageable, float damage)
        {
            damageable.TakeDamage(damage);
        }

        public override void PlayMeleeAttackAnimation()
        {
            
        }

        public virtual void OnCollisionEnter(Collision other)
        {
            Debug.Log(other.gameObject.name);
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                float damage = BaseDamage;
                DealDamage(damageable, damage);
            }
        }
    }
}
