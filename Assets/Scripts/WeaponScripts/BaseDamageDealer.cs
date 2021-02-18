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

        public void OnCollisionEnter(Collision other)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                float damage = BaseDamage;
                DealDamage(damageable, damage);
            }
        }
    }
}
