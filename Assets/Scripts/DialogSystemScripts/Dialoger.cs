using System;
using UnityEngine;
using UnityEngine.Events;

namespace DialogSystemScripts
{
    public class Dialoger : MonoBehaviour, ITalkable
    {
        [SerializeField] private UsualDialog _startDialog;
        [SerializeField] private UsualDialog _currentPhrase;
        private bool _isTalk = false;
        private PlayerDialoger _dialoger;

        public void StartDialog(PlayerDialoger dialoger)
        {
            _currentPhrase = _startDialog;
            _isTalk = true;
            _dialoger = dialoger;
            UpdateDialog();
        }

        public void UpdateDialog()
        {
            UpdateText();

            UnityAction actionA;
            UnityAction actionB;
            if (!_currentPhrase.OptionAPhrase) actionA = _dialoger.EndDialog;
            else actionA = () =>
            {
                _currentPhrase = _currentPhrase.OptionAPhrase;
                _currentPhrase.ChooseDialog(this);
            };
            if (!_currentPhrase.OptionBPhrase) actionB = _dialoger.EndDialog;
            else actionB = () =>
            {
                _currentPhrase = _currentPhrase.OptionBPhrase;
                _currentPhrase.ChooseDialog(this);
            };
            
            UpdateButtons(actionA, actionB);
        }

        private void UpdateButtons(UnityAction actionA, UnityAction actionB)
        {
            _dialoger.ButtonChooseA.ChooseButton.onClick.RemoveAllListeners();
            _dialoger.ButtonChooseB.ChooseButton.onClick.RemoveAllListeners();
            _dialoger.ButtonChooseA.ChooseButton.onClick.AddListener(actionA);
            _dialoger.ButtonChooseB.ChooseButton.onClick.AddListener(actionB);
        }

        private void UpdateText()
        {
            _dialoger.DialogPanel.DialogText.text = _currentPhrase.MainPhrase;
            _dialoger.ButtonChooseA.ButtonText.text =
                !_currentPhrase.OptionAPhrase ? "-" : _currentPhrase.OptionAPhrase.OptionPhrase;
            _dialoger.ButtonChooseB.ButtonText.text =
                !_currentPhrase.OptionBPhrase ? "-" : _currentPhrase.OptionBPhrase.OptionPhrase;
        }

        public void EndDialog()
        {
            _currentPhrase = _startDialog;
            _isTalk = false;
            _dialoger.ButtonChooseA.ChooseButton.onClick.RemoveAllListeners();
            _dialoger.ButtonChooseB.ChooseButton.onClick.RemoveAllListeners();
            _dialoger = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerDialoger dialoger)) dialoger.SetPressToTalkText(true, this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerDialoger dialoger))
            {
                if(_isTalk) dialoger.EndDialog();
                dialoger.SetPressToTalkText(false, this);
            }
        }
    }
}
