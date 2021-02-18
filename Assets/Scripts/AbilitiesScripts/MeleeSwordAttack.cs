using AbilitySupports;
using UnityEngine;

namespace AbilitiesScripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Melee Sword Attack")]
    public class MeleeSwordAttack : BaseActiveAbility
    {
        public override void Execute()
        {
            int num = Random.Range(1,4);
            switch (num)
            {
                case 1:
                    _mainHandAnimator.SetTrigger("MA 1");
                    break;
                case 2:
                    _mainHandAnimator.SetTrigger("MA 2");
                    break;
                case 3:
                    _mainHandAnimator.SetTrigger("MA 3");
                    break;
            }

            if (_offHandAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shield Block"))
            {
                _offHandAnimator.SetTrigger("Shield Bash");
            }
        }
    }
}
