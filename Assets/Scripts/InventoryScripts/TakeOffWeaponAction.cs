using WeaponScripts;

namespace InventoryScripts
{
    public class TakeOffWeaponAction : TakeOffEquippableItemAction
    {
        private readonly Weapon _weapon;
        private PlayerEquipment _playerEquipment;
        
        public TakeOffWeaponAction(UIController uiController, EquippableItem item,
            Inventory inventory, Weapon weapon, PlayerEquipment equipment) : base(uiController, item, inventory)
        {
            _weapon = weapon;
            _playerEquipment = equipment;
        }
        public override void Do()
        {
            base.Do();
            _playerEquipment.InitWeapons(_weapon.WeaponType, null);
            _equippableItem.gameObject.layer = 0;
        }
    }
}