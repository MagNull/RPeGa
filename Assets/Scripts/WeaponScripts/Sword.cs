using UnityEngine;
using Zenject;

namespace WeaponScripts
{
    public class Sword : Weapon
    {
        private ParticleSystem _particles;

        [Inject] 
        private DamageCalculator _damageCalculator;


        protected void Awake()
        {
            _particles = GetComponentInChildren<ParticleSystem>();
            if(_particles)_particles.enableEmission = false;
        }

        protected override void DealDamage(IDamageable damageable, float damage)
        {
            float resultDamage = _damageCalculator.CalculateDamage(damage);
            damageable.TakeDamage(resultDamage);
        }


        public override void InvertDamageState()
        {
            base.InvertDamageState();
            if(_particles)_particles.enableEmission = !_particles.emission.enabled;
        }

    }
}
