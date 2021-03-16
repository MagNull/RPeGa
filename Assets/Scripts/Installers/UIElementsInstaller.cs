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
        [SerializeField] private Canvas _canvas;
        public override void InstallBindings()
        {
            Container.Bind<ItemPanel>().FromInstance(_itemPanel).AsSingle();
            Container.Bind<InventoryUIController>().FromInstance(_canvas.GetComponent<InventoryUIController>()).AsSingle();
            Container.Bind<PlayerResourcesUIController>().FromInstance(_canvas.GetComponent<PlayerResourcesUIController>()).AsSingle();
            Container.Bind<EquipmentUIController>().FromInstance(_canvas.GetComponent<EquipmentUIController>()).AsSingle();
            Container.Bind<QuestsUIController>().FromInstance(_canvas.GetComponent<QuestsUIController>()).AsSingle();
        }
    }
}