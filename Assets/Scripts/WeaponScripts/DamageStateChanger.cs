using UnityEngine;

namespace WeaponScripts
{
    public class DamageStateChanger : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        public void ChangeDamageState()
        {
            _weapon.ChangeDamageState();
        }
    }
}
