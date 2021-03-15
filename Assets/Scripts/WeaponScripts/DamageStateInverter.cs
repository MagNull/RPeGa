using UnityEngine;

namespace WeaponScripts
{
    public class DamageStateInverter : MonoBehaviour
    {
        [SerializeField] private Transform _hand;

        public void InvertDamageState()
        {
            Transform child = _hand.GetChild(0);
            if (child.TryGetComponent(out Weapon weapon))
            {
                weapon.InvertDamageState();
            }
            else
            {
                Debug.Log($"There no weapon in {_hand.name}");
            }
        }
    }
}
