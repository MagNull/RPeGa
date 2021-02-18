using UnityEngine;
namespace WeaponScripts
{
    public abstract class Weapon : MonoBehaviour
    {
        protected abstract void DealDamage(IDamageable damageable, float damage);
    }
}