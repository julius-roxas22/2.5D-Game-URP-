using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/CheckTurn")]
    public class CheckRunningTurn : StateData
    {
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;

            if (control._IsFacingForward())
            {
                if (control._MoveLeft)
                {
                    animator.SetBool(_TransitionParameters.Turn.ToString(), true);
                }
            }
            else if (!control._IsFacingForward())
            {
                if (control._MoveRight)
                {
                    animator.SetBool(_TransitionParameters.Turn.ToString(), true);
                }
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(_TransitionParameters.Turn.ToString(), false);
        }
    }
}


