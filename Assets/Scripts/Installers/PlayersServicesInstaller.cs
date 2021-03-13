using AbilitySupports;
using InventoryScripts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayersServicesInstaller : MonoInstaller
    {
        [SerializeField] private PlayerMovement _player;
        public override void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromInstance(_player).AsSingle();
            Container.Bind<InputHandler>().FromInstance(_player.GetComponent<InputHandler>()).AsSingle();
            Container.Bind<PlayerSpeedManipulator>().FromInstance(_player.GetComponent<PlayerSpeedManipulator>()).AsSingle();
            Container.Bind<PlayerResources>().FromInstance(_player.GetComponent<PlayerResources>()).AsSingle();
            Container.Bind<DamageCalculator>().FromInstance(_player.GetComponent<DamageCalculator>()).AsSingle();
            Container.Bind<Inventory>().FromInstance(_player.GetComponent<Inventory>()).AsSingle();
            Container.Bind<PlayerEquipment>().FromInstance(_player.GetComponent<PlayerEquipment>()).AsSingle();
            Container.Bind<PlayerBonuses>().FromInstance(_player.GetComponent<PlayerBonuses>()).AsSingle();
        }
    }
}