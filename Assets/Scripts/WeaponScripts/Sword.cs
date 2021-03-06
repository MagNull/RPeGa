using UnityEngine;
using Zenject;

namespace WeaponScripts
{
    public class Sword : Weapon
    {
        [SerializeField] private float _baseDamage = 1;
        private ParticleSystem _particles;

        [Inject] 
        private DamageCalculator _damageCalculator;


        protected override void Awake()
        {
            base.Awake();
            _particles = GetComponentInChildren<ParticleSystem>();
            if(_particles)_particles.enableEmission = false;
        }

        public override void PlayMeleeAttackAnimation()
        {
            int num = Random.Range(1,4);
            switch (num)
            {
                case 1:
                    _animator.SetTrigger("MA 1");
                    break;
                case 2:
                    _animator.SetTrigger("MA 2");
                    break;
                case 3:
                    _animator.SetTrigger("MA 3");
                    break;
            }
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
            if (_canDamage)
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    DealDamage(damageable, _baseDamage);
                }  
            }
        }
    
    }
}
