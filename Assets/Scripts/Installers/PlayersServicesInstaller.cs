using AbilitySupports;
using InventoryScripts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayersServicesInstaller : MonoInstaller
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private PlayerSpeedManipulator _playerSpeedManipulator;
        [SerializeField] private PlayerResources _playerResources;
        [SerializeField] private DamageCalculator _damageCalculator;
        [SerializeField] private Inventory _inventory;
        public override void InstallBindings()
        {
            Container.Bind<InputHandler>().FromInstance(_inputHandler).AsSingle();
            Container.Bind<PlayerSpeedManipulator>().FromInstance(_playerSpeedManipulator).AsSingle();
            Container.Bind<PlayerResources>().FromInstance(_playerResources).AsSingle();
            Container.Bind<DamageCalculator>().FromInstance(_damageCalculator).AsSingle();
            Container.Bind<Inventory>().FromInstance(_inventory).AsSingle();
        }
    }
}