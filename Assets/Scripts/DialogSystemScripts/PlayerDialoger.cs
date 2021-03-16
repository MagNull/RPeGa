using DG.Tweening;
using Others;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DialogSystemScripts
{
    public class PlayerDialoger : MonoBehaviour
    {
        public DialogPanel DialogPanel => _dialogPanel;
        public DialogChooseButton ButtonChooseA => _buttonA;
        public DialogChooseButton ButtonChooseB => _buttonB;
        [SerializeField] private DialogPanel _dialogPanel;
        [SerializeField] private DialogChooseButton _buttonA;
        [SerializeField] private DialogChooseButton _buttonB;
        [SerializeField] private Text _pressToTalkText;
        [SerializeField] private Canvas _dialogCanvas;
        [Inject] private InputHandler _inputHandler;
        private ITalkable _talkTarget;
        private bool _isTalking;

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.R))
                .Subscribe(_ =>
                {
                    if (_talkTarget != null && !_isTalking) StartDialog();
                });
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Escape))
                .Subscribe(_ =>
                {
                    if (_talkTarget != null && _isTalking) EndDialog();
                });
            _dialogCanvas.gameObject.SetActive(false);
            _pressToTalkText.gameObject.SetActive(false);
        }

        public void SetPressToTalkText(bool state, ITalkable target)
        {
            if (state) _talkTarget = target;
            else _talkTarget = null;
            _pressToTalkText.gameObject.SetActive(state);
        }

        private void StartDialog()
        {
            _isTalking = true;
            _talkTarget.StartDialog(this);
            ChangeDialogWindowEnable();
        }

        public void EndDialog()
        {
            _isTalking = false;
            ChangeDialogWindowEnable();
            _talkTarget.EndDialog();
        }
        private void ChangeDialogWindowEnable()
        {
            bool state = _isTalking;
            if ((_inputHandler.CanAttack && _inputHandler.CanCast) == state)
            {
                _dialogCanvas.gameObject.SetActive(state);
                _pressToTalkText.gameObject.SetActive(!state);
                _dialogCanvas.transform.localScale = Vector3.zero;
                if(state) _dialogCanvas.gameObject.transform.DOScale(Vector3.one, .5f);
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
