using UnityEngine;

namespace WeaponScripts
{
    public class DamageStateChanger : MonoBehaviour
    {
        [SerializeField] private Transform _hand;

        public void ChangeDamageState()
        {
            Transform child = _hand.GetChild(0);
            if (child.TryGetComponent(out Weapon weapon))
            {
                weapon.ChangeDamageState();
            }
            else
            {
                Debug.Log(child.name);
            }
        }
    }
}
