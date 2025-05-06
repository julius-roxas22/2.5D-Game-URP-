using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/CheckTurbo")]
    public class CheckTurbo : StateData
    {
        [SerializeField] private bool mustRequireMovement;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);
            if (control._Turbo)
            {
                if (mustRequireMovement)
                {
                    if (control._MoveLeft || control._MoveRight)
                    {
                        animator.SetBool(_TransitionParameters.Turbo.ToString(), true);
                    }
                    else
                    {
                        animator.SetBool(_TransitionParameters.Turbo.ToString(), false);
                    }
                }
                else
                {
                    animator.SetBool(_TransitionParameters.Turbo.ToString(), true);
                }
            }
            else
            {
                animator.SetBool(_TransitionParameters.Turbo.ToString(), false);
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


