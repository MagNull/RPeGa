using AbilitySupports;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayersServicesInstaller : MonoInstaller
    {
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private PlayerSpeedManipulator playerSpeedManipulator;
        [SerializeField] private PlayerResources playerResources;
        [SerializeField] private DamageCalculator damageCalculator;
        public override void InstallBindings()
        {
            Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
            Container.Bind<PlayerSpeedManipulator>().FromInstance(playerSpeedManipulator).AsSingle();
            Container.Bind<PlayerResources>().FromInstance(playerResources).AsSingle();
            Container.Bind<DamageCalculator>().FromInstance(damageCalculator).AsSingle();
        }
    }
}