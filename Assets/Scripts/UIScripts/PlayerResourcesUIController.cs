using AbilitySupports;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UIScripts
{
    public class PlayerResourcesUIController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Slider _manaSlider;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Text _manaText;
        [SerializeField] private Text _healthText;

        [Inject] private PlayerResources _playerResources;

        private void Start()
        {
            _playerResources
                .CurrentHealth
                .Where(x => x >= 0)
                .Subscribe(_ => ChangeHealthSliderValue())
                .AddTo(this);
            _playerResources
                .MAXHealth
                .Where(x => x >= 0)
                .Subscribe(_ => ChangeHealthSliderValue())
                .AddTo(this);
            _playerResources
                .CurrentMana
                .Where(x => x >= 0)
                .Subscribe(_ => ChangeManaSliderValue())
                .AddTo(this);
            _playerResources
                .MAXMana
                .Where(x => x >= 0)
                .Subscribe(_ => ChangeManaSliderValue())
                .AddTo(this);
        }
    
        private void ChangeHealthSliderValue()
        {
            _healthSlider.value = _playerResources.CurrentHealth.Value / _playerResources.MAXHealth.Value;
            _healthText.text = $"{(int)_playerResources.CurrentHealth.Value} / {(int)_playerResources.MAXHealth.Value}";
        }
    
        private void ChangeManaSliderValue()
        {
            _manaSlider.value = _playerResources.CurrentMana.Value / _playerResources.MAXMana.Value;
            _manaText.text = $"{(int)_playerResources.CurrentMana.Value} / {(int)_playerResources.MAXMana.Value}";
        }
    }
}
