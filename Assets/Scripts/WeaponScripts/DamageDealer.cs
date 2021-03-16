using System;
using Others;
using UnityEngine;

namespace WeaponScripts
{
    public class DamageDealer : MonoBehaviour
    {
        public float BaseDamage = 1;
        [SerializeField] protected bool _canDamage = true;

        protected virtual void DealDamage(IDamageable damageable, float damage)
        {
            damageable.TakeDamage(damage);
        }
        
        public virtual void InvertDamageState()
        {
            _canDamage = !_canDamage;
        }

        public virtual void OnCollisionEnter(Collision other)
        {
            if (_canDamage && other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                float damage = BaseDamage;
                DealDamage(damageable, damage);
            }
        }
    }
}
