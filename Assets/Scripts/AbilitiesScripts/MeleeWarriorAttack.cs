using AbilitySupports;
using UniRx;
using UnityEngine;

namespace AbilitiesScripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Melee Sword Attack")]
    public class MeleeWarriorAttack : BaseActiveAbility
    {
        public override void Execute(ReactiveProperty<float> mana)
        {
            if (!(_mainHandWeapon is null)) _mainHandWeapon.PlayMeleeAttackAnimation();

            if (!(_offHandWeapon is null)) _offHandWeapon.SetSkillTrigger("Shield Bash");
        }
    }
}
