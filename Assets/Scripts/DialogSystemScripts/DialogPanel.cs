using System;
using UnityEngine;
using UnityEngine.UI;

namespace DialogSystemScripts
{
    public class DialogPanel : MonoBehaviour
    {
        private Text _dialogText;

        public Text DialogText => _dialogText;

        private void Awake()
        {
            _dialogText = GetComponentInChildren<Text>();
        }
    }
}