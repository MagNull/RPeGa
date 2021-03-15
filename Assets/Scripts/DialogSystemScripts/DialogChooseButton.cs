using System;
using UnityEngine;
using UnityEngine.UI;

namespace DialogSystemScripts
{
    public class DialogChooseButton : MonoBehaviour
    {
        private Button _chooseButton;
        private Text _buttonText;

        public Button ChooseButton => _chooseButton;

        public Text ButtonText => _buttonText;

        private void Awake()
        {
            _chooseButton = GetComponent<Button>();
            _buttonText = GetComponentInChildren<Text>();
        }
    }
}