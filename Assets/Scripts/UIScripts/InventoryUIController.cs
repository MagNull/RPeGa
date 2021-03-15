using DG.Tweening;
using Other;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace UIScripts
{
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [Inject] private InputHandler _inputHandler;
    
        private void Start()
        {
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
                SetPlayerActivityState(!state);
            }
        }

        private void SetPlayerActivityState(bool state)
        {
            _inputHandler.CanAttack = state;
            _inputHandler.CanCast = state;
        }
    }
}
