using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayersServicesInstaller : MonoInstaller
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Slider manaSlider;
    [SerializeField] private PlayerSpeedManipulator playerSpeedManipulator;
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<PlayerSpeedManipulator>().FromInstance(playerSpeedManipulator).AsSingle();
        Container.Bind<Slider>().WithId("Mana").FromInstance(manaSlider).AsSingle();
    }
}