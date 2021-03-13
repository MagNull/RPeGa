using UnityEngine;

namespace AbilitySupports
{
    public class RefreshTriggers : StateMachineBehaviour
    {
        [SerializeField] private string[] _triggers;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (string triggerName in _triggers)
            {
                animator.ResetTrigger(triggerName);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (string triggerName in _triggers)
            {
                animator.ResetTrigger(triggerName);
            }
        }
    }
}
