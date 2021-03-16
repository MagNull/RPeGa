using System;
using Other;
using UnityEngine;
using WeaponScripts;
using Zenject;

namespace InventoryScripts
{
    [RequireComponent(typeof(EquippableItem))]
    public class ItemBonus : MonoBehaviour
    {
        [SerializeField] private float _damageBonus;
        [SerializeField] private float _speedBonus;
        [SerializeField] private float _armorBonus;
        [SerializeField] private float _manaBonus;
        [SerializeField] private float _healthBonus;
        [SerializeField] private int _strengthBonus;
        [SerializeField] private int _agilityBonus;
        [SerializeField] private int _intelligenceBonus;
        private EquippableItem _equippableItem;

        [Inject] private PlayerBonuses _playerBonuses;
        private void Awake()
        {
            _equippableItem = GetComponent<EquippableItem>();
        }

        private void OnEnable()
        {
            _equippableItem.OnTakeOffItem += RemoveBonus;
            _equippableItem.OnPutOnItem += AddBonus;
        }

        private void OnDisable()
        {
            _equippableItem.OnTakeOffItem -= RemoveBonus;
            _equippableItem.OnPutOnItem -= AddBonus;
        }

        private void RemoveBonus()
        {
            _playerBonuses.DamageBonus.Value -= _damageBonus;
            _playerBonuses.SpeedBonus.Value -= _speedBonus;
            _playerBonuses.ArmorBonus.Value -= _armorBonus;
            _playerBonuses.ManaBonus.Value -= _manaBonus;
            _playerBonuses.PlayerStats.Intelligence -= _intelligenceBonus;
            _playerBonuses.PlayerStats.Strength -= _strengthBonus;
            _playerBonuses.PlayerStats.Agility -= _agilityBonus;
        }

        private void AddBonus()
        {
            _playerBonuses.DamageBonus.Value += _damageBonus;
            _playerBonuses.SpeedBonus.Value += _speedBonus;
            _playerBonuses.ArmorBonus.Value += _armorBonus;
            _playerBonuses.ManaBonus.Value += _manaBonus;
            _playerBonuses.HealthBonus.Value += _healthBonus;
            _playerBonuses.PlayerStats.Intelligence += _intelligenceBonus;
            _playerBonuses.PlayerStats.Strength += _strengthBonus;
            _playerBonuses.PlayerStats.Agility += _agilityBonus;
        }
        
    }
}
