using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySupports;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider manaSlider;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Text manaText;
    [SerializeField] private Text healthText;

    [Inject]
    private PlayerResources _playerResources;

    private void Start()
    {
        _playerResources
            .Health
            .Where(x => x >= 0)
            .Subscribe(x => ChangeHealthSliderValue(x))
            .AddTo(this);
        _playerResources
            .Mana
            .Where(x => x >= 0)
            .Subscribe(x => ChangeManaSliderValue(x))
            .AddTo(this);
    }

    private void ChangeHealthSliderValue(float health)
    {
        healthSlider.value = health / _playerResources.MAXHealth.Value;
        healthText.text = String.Format("{0} / {1}", health, _playerResources.MAXHealth.Value);
    }


    private void ChangeManaSliderValue(float mana)
    {
        manaSlider.value = mana / _playerResources.MAXMana.Value;
        manaText.text = String.Format("{0} / {1}", mana, _playerResources.MAXMana.Value);
    }
}
