using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/AI_Ability/StartWalking")]
    public class StartWalking : StateData
    {

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            characterStateBase._CharacterControl._GetAiController.WalkStraightTowardsToStartsSPhere();
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;

            float dist = control._GetAiProgress._GetDistanceToStartSphere();

            if (control._GetAiProgress._EndSphereIsHigher())
            {
                if (dist < 0.01f)
                {
                    control._MoveRight = false;
                    control._MoveLeft = false;

                    animator.SetBool(AITransitions.jump_platform.ToString(), true);
                    return;
                }
            }

            if (control._GetAiProgress._EndSphereIsLower())
            {
                animator.SetBool(AITransitions.fall_platform.ToString(), true);
                return;
            }

            if (dist > 3f)
            {
                control._Turbo = true;
            }
            else
            {
                control._Turbo = false;
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(AITransitions.jump_platform.ToString(), false);
            animator.SetBool(AITransitions.fall_platform.ToString(), false);
        }
    }
}


