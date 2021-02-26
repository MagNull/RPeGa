using System;
using AbilitySupports;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider _manaSlider;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Text _manaText;
    [SerializeField] private Text _healthText;
    [SerializeField] private GameObject _panel;

    
    private PlayerResources _playerResources;
    private InputHandler _inputHandler;

    [Inject]
    public void Construct(PlayerResources playerResources, InputHandler inputHandler)
    {
        _playerResources = playerResources;
        _inputHandler = inputHandler;
    }

    private void Start()
    {
        _playerResources
            .CurrentHealth
            .Where(x => x >= 0)
            .Subscribe(x => ChangeHealthSliderValue(x))
            .AddTo(this);
        _playerResources
            .CurrentMana
            .Where(x => x >= 0)
            .Subscribe(x => ChangeManaSliderValue(x))
            .AddTo(this);
        
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.I))
            .Subscribe(_ => ChangeInventoryEnable());
        
        _panel.SetActive(false);
    }

    private void ChangeInventoryEnable()
    {
        bool state = !_panel.activeSelf;
        _panel.SetActive(state);
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
        StopPlayerActivity(!state);
    }

    private void StopPlayerActivity(bool state)
    {
        _inputHandler.CanAttack = state;
        _inputHandler.CanCast = state;
    }

    private void ChangeHealthSliderValue(float health)
    {
        _healthSlider.value = health / _playerResources.MAXHealth.Value;
        _healthText.text = $"{health} / {_playerResources.MAXHealth.Value}";
    }


    private void ChangeManaSliderValue(float mana)
    {
        _manaSlider.value = mana / _playerResources.MAXMana.Value;
        _manaText.text = $"{mana} / {_playerResources.MAXMana.Value}";
    }
}
