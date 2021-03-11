using System;
using UnityEngine;
using Zenject;

namespace InventoryScripts
{
    [RequireComponent(typeof(EquipableItem))]
    public class ItemBonus : MonoBehaviour
    {
        [SerializeField] private float _damageBonus;
        [SerializeField] private float _speedBonus;
        [SerializeField] private float _armorBonus;
        [SerializeField] private float _manaBonus;
        [SerializeField] private float _healthBonus;
        private EquipableItem _equipableItem;

        [Inject] private PlayerBonuses _playerBonuses;
        private void Awake()
        {
            _equipableItem = GetComponent<EquipableItem>();
        }

        private void OnEnable()
        {
            _equipableItem.OnTakeOffItem += RemoveBonus;
            _equipableItem.OnDropItem += RemoveBonus;
            _equipableItem.OnPutOnItem += AddBonus;
        }

        private void OnDisable()
        {
            _equipableItem.OnTakeOffItem -= RemoveBonus;
            _equipableItem.OnDropItem -= RemoveBonus;
            _equipableItem.OnPutOnItem -= AddBonus;
        }

        private void RemoveBonus(EquipableType equipableType)
        {
            _playerBonuses.DamageBonus.Value -= _damageBonus;
            _playerBonuses.SpeedBonus.Value -= _speedBonus;
            _playerBonuses.ArmorBonus.Value -= _armorBonus;
            _playerBonuses.ManaBonus.Value -= _manaBonus;
            _playerBonuses.HealthBonus.Value -= _healthBonus;
        }
        
        private void RemoveBonus()
        {
            _playerBonuses.DamageBonus.Value -= _damageBonus;
            _playerBonuses.SpeedBonus.Value -= _speedBonus;
            _playerBonuses.ArmorBonus.Value -= _armorBonus;
            _playerBonuses.ManaBonus.Value -= _manaBonus;
            _playerBonuses.HealthBonus.Value -= _healthBonus;
        }

        private void AddBonus()
        {
            _playerBonuses.DamageBonus.Value += _damageBonus;
            _playerBonuses.SpeedBonus.Value += _speedBonus;
            _playerBonuses.ArmorBonus.Value += _armorBonus;
            _playerBonuses.ManaBonus.Value += _manaBonus;
            _playerBonuses.HealthBonus.Value += _healthBonus;
        }
        
    }
}
