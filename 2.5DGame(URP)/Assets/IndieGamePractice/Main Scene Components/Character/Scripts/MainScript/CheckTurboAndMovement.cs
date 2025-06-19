using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/CheckTurboAndMovement")]
    public class CheckTurboAndMovement : StateData
    {
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            if ((control._MoveLeft || control._MoveRight) && control._Turbo)
            {
                animator.SetBool(_TransitionParameters.Move.ToString(), true);
                animator.SetBool(_TransitionParameters.Turbo.ToString(), true);
            }
            else
            {
                animator.SetBool(_TransitionParameters.Move.ToString(), false);
                animator.SetBool(_TransitionParameters.Turbo.ToString(), false);
            }

        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


