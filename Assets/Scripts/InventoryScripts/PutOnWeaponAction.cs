using UnityEngine;
using WeaponScripts;

namespace InventoryScripts
{
    public class PutOnWeaponAction : PutOnEquippableItemAction
    {
        private Weapon _weapon;
        public PutOnWeaponAction(UIController uiController, EquippableItem item, Inventory inventory,
            PlayerEquipment playerEquipment, Weapon weapon) : base(uiController, item, inventory, playerEquipment)
        {
            _weapon = weapon;
        }
        public override void Do()
        {
            base.Do();
            _equippableItem.gameObject.layer = 7;
            _playerEquipment.InitWeapons(_weapon.WeaponType, _weapon);
        }
    }
}