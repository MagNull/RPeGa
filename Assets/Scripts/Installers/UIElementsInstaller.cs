using AbilitySupports;
using InventoryScripts;
using UIScripts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIElementsInstaller : MonoInstaller
    {
        [SerializeField] private ItemPanel _itemPanel;
        [SerializeField] private InventoryUIController _inventoryUIController;
        [SerializeField] private PlayerResourcesUIController _playerResourcesUIController;
        [SerializeField] private EquipmentUIController _equipmentUIController;
        public override void InstallBindings()
        {
            Container.Bind<ItemPanel>().FromInstance(_itemPanel).AsSingle();
            Container.Bind<InventoryUIController>().FromInstance(_inventoryUIController).AsSingle();
            Container.Bind<PlayerResourcesUIController>().FromInstance(_playerResourcesUIController).AsSingle();
            Container.Bind<EquipmentUIController>().FromInstance(_equipmentUIController).AsSingle();
        }
    }
}