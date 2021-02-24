using UnityEngine;
using Zenject;

namespace WeaponScripts
{
    public class Sword : Weapon
    {
        [SerializeField] private float baseDamage = 1;
        private ParticleSystem _particles;

        [Inject] 
        private DamageCalculator _damageCalculator;


        private void Awake()
        {
            _particles = GetComponentInChildren<ParticleSystem>();
            if(_particles)_particles.enableEmission = false;
        }

        protected override void DealDamage(IDamageable damageable, float damage)
        {
            float resultDamage = _damageCalculator.CalculateDamage(damage);
            damageable.TakeDamage(resultDamage);
        }
        

        public override void ChangeDamageState()
        {
            base.ChangeDamageState();
            if(_particles)_particles.enableEmission = !_particles.emission.enabled;
        }
    
        public void OnCollisionEnter(Collision other)
        {
            if (canDamage)
            {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    DealDamage(damageable, baseDamage);
                }  
            }
        }
    
    }
}
