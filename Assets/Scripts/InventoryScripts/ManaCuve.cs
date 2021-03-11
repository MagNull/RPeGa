using AbilitySupports;
using UnityEngine;
using Zenject;

namespace InventoryScripts
{
    public class ManaCuve : СonsumableItem
    {
        [SerializeField] private int _manaRefill = 1;
        [Inject] private PlayerResources _playerResources;
        public override void Use()
        {
            _playerResources.CurrentMana.Value += _manaRefill;
            base.Use();
        }
    }
}
