using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ManaSliderInstaller : MonoInstaller
{
    [SerializeField] private Slider manaSlider;
    public override void InstallBindings()
    {
        Container.Bind<Slider>().WithId("Mana").FromInstance(manaSlider).AsSingle();
    }
}