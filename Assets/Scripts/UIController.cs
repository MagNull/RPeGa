using System;
using AbilitySupports;
using InventoryScripts;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using WeaponScripts;
using Zenject;
using DG;
using DG.Tweening;
public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider _manaSlider;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Text _manaText;
    [SerializeField] private Text _healthText;
    [SerializeField] private GameObject _panel;

    [Header("Equipment Slots UI")] [SerializeField]
    private EquipmentSlot[] _equipmentSlots;

    private PlayerResources _playerResources;
    private InputHandler _inputHandler;

    [Inject]
    public void Construct(PlayerResources playerResources, InputHandler inputHandler)
    {
        _playerResources = playerResources;
        _inputHandler = inputHandler;
    }

    public EquipmentSlot SetAndGetEquipmentInSlot(EquippableItem item)
    {
        int i = 0;
        while (_equipmentSlots[i].Index != item.ItemPlaceIndex) i++;
        _equipmentSlots[i].SetItem(item);
        return _equipmentSlots[i];
    }

    public void RemoveEquipmentFromSlot(EquippableItem item)
    {
        int i = 0;
        while (_equipmentSlots[i].Index != item.ItemPlaceIndex) i++;
        _equipmentSlots[i].DeleteItem();
    }

    private void Start()
    {
        _playerResources
            .CurrentHealth
            .Where(x => x >= 0)
            .Subscribe(_ => ChangeHealthSliderValue())
            .AddTo(this);
        _playerResources
            .CurrentMana
            .Where(x => x >= 0)
            .Subscribe(_ => ChangeManaSliderValue())
            .AddTo(this);
        
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.I))
            .Subscribe(_ => ChangeInventoryEnable());
        
        _panel.SetActive(false);
    }

    private void ChangeInventoryEnable()
    {
        bool state = !_panel.activeSelf;
        if ((_inputHandler.CanAttack && _inputHandler.CanCast) == state)
        {
            _panel.SetActive(state);
            _panel.transform.localScale = Vector3.zero;
            if(state) _panel.transform.DOScale(Vector3.one, .5f);
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = state;
            StopPlayerActivity(!state);
        }
    }

    private void StopPlayerActivity(bool state)
    {
        _inputHandler.CanAttack = state;
        _inputHandler.CanCast = state;
    }

    private void ChangeHealthSliderValue()
    {
        _healthSlider.value = _playerResources.CurrentHealth.Value / _playerResources.MAXHealth.Value;
        _healthText.text = $"{_playerResources.CurrentHealth.Value} / {_playerResources.MAXHealth.Value}";
    }
    
    private void ChangeManaSliderValue()
    {
        _manaSlider.value = _playerResources.CurrentMana.Value / _playerResources.MAXMana.Value;
        _manaText.text = $"{_playerResources.CurrentMana.Value} / {_playerResources.MAXMana.Value}";
    }
}
