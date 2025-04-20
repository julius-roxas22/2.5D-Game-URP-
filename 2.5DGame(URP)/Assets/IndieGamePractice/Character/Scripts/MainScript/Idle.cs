using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/Idle")]
    public class Idle : StateData
    {
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(TransitionParameters.Jump.ToString(), false);
            animator.SetBool(TransitionParameters.Attack.ToString(), false);
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);

            if (control._MoveRight && control._MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (control._Attack)
            {
                animator.SetBool(TransitionParameters.Attack.ToString(), true);
            }

            if (control._Jump)
            {
                animator.SetBool(TransitionParameters.Jump.ToString(), true);
            }

            if (control._MoveRight)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), true);
            }

            if (control._MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), true);
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


