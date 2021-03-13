using System;
using UnityEngine;
namespace WeaponScripts
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected bool _canDamage = false;
        protected Animator _animator;


        protected virtual void Awake()
        {
            _animator = GetComponentInParent<Animator>();
        }

        public abstract void PlayMeleeAttackAnimation();
        
        protected abstract void DealDamage(IDamageable damageable, float damage);

        public virtual void ChangeDamageState()
        {
            _canDamage = !_canDamage;
        }
    }
}