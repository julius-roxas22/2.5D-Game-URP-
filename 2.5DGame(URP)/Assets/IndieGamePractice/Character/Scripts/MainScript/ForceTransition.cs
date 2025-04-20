using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/ForceTransition")]
    public class ForceTransition : StateData
    {
        [Range(0.01f, 1f)]
        [SerializeField] private float transitionTiming;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (animatorStateInfo.normalizedTime >= transitionTiming)
            {
                animator.SetBool(TransitionParameters.ForceTransition.ToString(), true);
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(TransitionParameters.ForceTransition.ToString(), false);
        }
    }
}


