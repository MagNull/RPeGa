using UnityEngine;
namespace WeaponScripts
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected bool _canDamage = false;
        protected abstract void DealDamage(IDamageable damageable, float damage);
        
        public virtual void ChangeDamageState()
        {
            _canDamage = !_canDamage;
        }
    }
}