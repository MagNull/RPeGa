using UnityEngine;

namespace DialogSystemScripts
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Phrase")]
    public class Dialog : ScriptableObject
    {
        [SerializeField] private string _mainPhrase;
        [SerializeField] private string _optionPhrase;
        [SerializeField] private Dialog _optionAPhrase;
        [SerializeField] private Dialog _optionBPhrase;

        public string MainPhrase => _mainPhrase;

        public Dialog OptionAPhrase => _optionAPhrase;

        public Dialog OptionBPhrase => _optionBPhrase;

        public string OptionPhrase => _optionPhrase;
    }
}
