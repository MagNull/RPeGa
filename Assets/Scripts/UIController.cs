using System;
using AbilitySupports;
using InventoryScripts;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using WeaponScripts;
using Zenject;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider _manaSlider;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Text _manaText;
    [SerializeField] private Text _healthText;
    [SerializeField] private GameObject _panel;

    [Header("Equipment Slots UI")]
    [SerializeField] private Slot _mainHandSlot;
    [SerializeField] private Slot _offHandSlot;
    [SerializeField] private Slot _headSlot;
    [SerializeField] private Slot _bodySlot;
    [SerializeField] private Slot _glovesSlot;
    [SerializeField] private Slot _legsSlot;
    [SerializeField] private Slot _bootsSlot;
    
    private PlayerResources _playerResources;
    private InputHandler _inputHandler;

    [Inject]
    public void Construct(PlayerResources playerResources, InputHandler inputHandler)
    {
        _playerResources = playerResources;
        _inputHandler = inputHandler;
    }

    public void SetEquipmentInSlot(EquipableType type, EquipableItem equipment)
    {
        switch (type)
        {
            case EquipableType.MAINHANDWEAPON:
                _mainHandSlot.SetItem(equipment);
                break;
            case EquipableType.OFFHANDWEAPON:
                _offHandSlot.SetItem(equipment);
                break;
            case EquipableType.HEAD:
                _headSlot.SetItem(equipment);
                break;
            case EquipableType.BODY:
                _bodySlot.SetItem(equipment);
                break;
            case EquipableType.GLOVES:
                _glovesSlot.SetItem(equipment);
                break;
            case EquipableType.LEGS:
                _legsSlot.SetItem(equipment);
                break;
            case EquipableType.BOOTS:
                _bootsSlot.SetItem(equipment);
                break;
        }
    }

    public void RemoveEquipmentFromSlot(EquipableType type)
    {
        switch (type)
        {
            case EquipableType.MAINHANDWEAPON:
                _mainHandSlot.DeleteItem();
                break;
            case EquipableType.OFFHANDWEAPON:
                _offHandSlot.DeleteItem();
                break;
            case EquipableType.HEAD:
                _headSlot.DeleteItem();
                break;
            case EquipableType.BODY:
                _bodySlot.DeleteItem();
                break;
            case EquipableType.GLOVES:
                _glovesSlot.DeleteItem();
                break;
            case EquipableType.LEGS:
                _legsSlot.DeleteItem();
                break;
            case EquipableType.BOOTS:
                _bootsSlot.DeleteItem();
                break;
        }
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
