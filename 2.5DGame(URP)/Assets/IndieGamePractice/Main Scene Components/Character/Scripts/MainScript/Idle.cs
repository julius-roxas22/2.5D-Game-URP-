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
            animator.SetBool(_TransitionParameters.Jump.ToString(), false);
            animator.SetBool(_TransitionParameters.Attack.ToString(), false);
            animator.SetBool(_TransitionParameters.Move.ToString(), false);
            animator.SetBool(_TransitionParameters.Grounded.ToString(), true);

            CharacterControl control = characterStateBase._CharacterControl;
            control._GetAnimationProgress._DisAllowEarlyTurn = false;
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;

            control._GetAnimationProgress._LockDirectionNextState = false;

            if (control._MoveRight && control._MoveLeft)
            {
                animator.SetBool(_TransitionParameters.Move.ToString(), false);
                return;
            }

            if (control._GetAnimationProgress._AttackTriggered)
            {
                animator.SetBool(_TransitionParameters.Attack.ToString(), true);
            }

            if (control._Jump)
            {
                if (!control._GetAnimationProgress._IsJumped)
                {
                    animator.SetBool(_TransitionParameters.Jump.ToString(), true);
                }
            }
            else
            {
                if (!control._GetAnimationProgress._IsRunningAbilities(typeof(Jump), this))
                {
                    control._GetAnimationProgress._IsJumped = false;
                }
            }

            if (control._MoveRight)
            {
                animator.SetBool(_TransitionParameters.Move.ToString(), true);
            }

            if (control._MoveLeft)
            {
                animator.SetBool(_TransitionParameters.Move.ToString(), true);
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(_TransitionParameters.Attack.ToString(), false);
            animator.SetBool(_TransitionParameters.Grounded.ToString(), true);
        }
    }
}


