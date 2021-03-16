using UnityEngine;

namespace DialogSystemScripts
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Usual Phrase")]
    public class UsualDialog : ScriptableObject, IDialog
    {
        [SerializeField] private string _mainPhrase;
        [SerializeField] private string _optionPhrase;
        [SerializeField] private UsualDialog _optionAPhrase;
        [SerializeField] private UsualDialog _optionBPhrase;

        public string MainPhrase => _mainPhrase;

        public UsualDialog OptionAPhrase => _optionAPhrase;

        public UsualDialog OptionBPhrase => _optionBPhrase;

        public string OptionPhrase => _optionPhrase;

        public virtual void ChooseDialog(Dialoger dialoger)
        {
            dialoger.UpdateDialog();
            Debug.Log(1);
        }
    }
}
