using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGamePractice
{
    public enum AICondition
    {
        RUN_TO_WALK,
    }

    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/AI_Ability/AITransitionCondition")]
    public class AITransitionCondition : StateData
    {

        public AICondition _AICondition;
        public AI_Type _NextAIType;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);

            if (TransitionToNextAI(control))
            {
                control._GetAiController.TriggerAI(_NextAIType);
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        private bool TransitionToNextAI(CharacterControl control)
        {
            if (_AICondition == AICondition.RUN_TO_WALK)
            {
                float dist = (control._GetAiProgress.agent._StartSphere.transform.position - control.transform.position).sqrMagnitude;
                if (dist < 2f)
                {
                    return true;
                }
            }

            return false;
        }
    }
}


