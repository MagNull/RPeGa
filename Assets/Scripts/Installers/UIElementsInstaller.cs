using InventoryScripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIElementsInstaller : MonoInstaller
{
    [SerializeField] private ItemPanel _itemPanel;
    public override void InstallBindings()
    {
        Container.Bind<ItemPanel>().FromInstance(_itemPanel).AsSingle();
    }
}