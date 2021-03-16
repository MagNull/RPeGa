using System;
using Others;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

namespace AbilitySupports
{
    public class PlayerResources : MonoBehaviour, IDamageable
    {
        public ReactiveProperty<float> MAXHealth = new ReactiveProperty<float>();
        public ReactiveProperty<float> MAXMana = new ReactiveProperty<float>();
        public ReactiveProperty<float> CurrentMana = new ReactiveProperty<float>(0);
        public ReactiveProperty<float> CurrentHealth = new ReactiveProperty<float>(0);
        [Inject] private PlayerBonuses _playerBonuses;

        public void Init()
        {
            CurrentMana.Value = MAXHealth.Value;
            CurrentHealth.Value = MAXHealth.Value;
        }

        private void Awake()
        {
            CurrentMana
                .Where(x => x < 0 || x >= MAXMana.Value)
                .Subscribe(_ =>
                {
                    CurrentMana.Value = Mathf.Clamp(CurrentMana.Value, 0, MAXMana.Value);
                });
            CurrentHealth
                .Where(x => x < 0 || x >= MAXHealth.Value)
                .Subscribe(_ =>
                {
                    CurrentHealth.Value = Mathf.Clamp(CurrentHealth.Value, 0, MAXHealth.Value);
                });
        }

        public void TakeDamage(float damage)
        {
            damage -= _playerBonuses.ArmorBonus.Value;
            damage = Mathf.Clamp(damage, 0, Single.MaxValue);
            CurrentHealth.Value -= damage;
        }
    }
}