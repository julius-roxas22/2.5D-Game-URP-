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
            CharacterControl control = characterStateBase._GetCharacterControl(animator);

            if (control._GetAiProgress.agent._StartSphere.transform.position.z > control.transform.position.z)
            {
                control._MoveRight = true;
                control._MoveLeft = false;
            }
            else
            {
                control._MoveRight = false;
                control._MoveLeft = true;
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);

            float dist = (control._GetAiProgress.agent._StartSphere.transform.position - control.transform.position).sqrMagnitude;
            if (dist < 0.01f)
            {
                control._MoveRight = false;
                control._MoveLeft = false;

                if (control._GetAiProgress.agent._StartSphere.transform.position.y < control._GetAiProgress.agent._EndSphere.transform.position.y)
                {
                    animator.SetBool(AITransitions.jump_platform.ToString(), true);
                }

            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(AITransitions.jump_platform.ToString(), false);
        }
    }
}


