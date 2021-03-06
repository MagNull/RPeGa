using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class ItemPanel : MonoBehaviour
    {
        [SerializeField] private Button _useButton;
        [SerializeField] private Button _dropButton;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void BindButtons(UnityAction useAction, UnityAction dropAction)
        {
            _useButton.onClick.AddListener(useAction);
            _useButton.onClick.AddListener(() => gameObject.SetActive(false));
            _dropButton.onClick.AddListener(() => gameObject.SetActive(false));
            _dropButton.onClick.AddListener(dropAction);
        }

        public void UnbindButtons()
        {
            _useButton.onClick.RemoveAllListeners();
            _dropButton.onClick.RemoveAllListeners();
        }

        private void OnDisable()
        {
            _useButton.onClick.RemoveAllListeners();
            _dropButton.onClick.RemoveAllListeners();
        }
    }
}
