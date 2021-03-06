using InventoryScripts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIElementsInstaller : MonoInstaller
    {
        [SerializeField] private ItemPanel _itemPanel;
        [SerializeField] private UIController _uiController;
        public override void InstallBindings()
        {
            Container.Bind<ItemPanel>().FromInstance(_itemPanel).AsSingle();
            Container.Bind<UIController>().FromInstance(_uiController).AsSingle();
        }
    }
}