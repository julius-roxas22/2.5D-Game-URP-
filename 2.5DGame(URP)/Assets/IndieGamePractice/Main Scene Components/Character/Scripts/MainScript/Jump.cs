using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/Jump")]
    public class Jump : StateData
    {
        [Range(0f, 1f)]
        [SerializeField] private float jumpTiming;
        [SerializeField] private float jumpForce;
        [SerializeField] private AnimationCurve pull;
        [SerializeField] private bool cancelPullGravity;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            control._GetAnimationProgress._IsJumped = false;
            if (jumpTiming == 0)
            {
                control._GetRigidBody.AddForce(Vector3.up * jumpForce);
                control._GetAnimationProgress._IsJumped = true;
            }
            animator.SetBool(_TransitionParameters.Grounded.ToString(), false);

            control._GetAnimationProgress._CancelPullGravity = cancelPullGravity;
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            if (!control._GetAnimationProgress._IsJumped && animatorStateInfo.normalizedTime >= jumpTiming)
            {
                control._GetRigidBody.AddForce(Vector3.up * jumpForce);
                control._GetAnimationProgress._IsJumped = true;
            }
            //control._PullMultiplier = pull.Evaluate(animatorStateInfo.normalizedTime);
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            //CharacterControl control = characterStateBase._CharacterControl;
            //control._PullMultiplier = 0f;
        }
    }
}


