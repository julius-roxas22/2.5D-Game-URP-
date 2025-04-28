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
        private bool isJumped;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (jumpTiming == 0)
            {
                characterStateBase._GetCharacterControl(animator)._GetRigidBody.AddForce(Vector3.up * jumpForce);
                isJumped = true;
            }
            animator.SetBool(_TransitionParameters.Grounded.ToString(), false);
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);
            if (!isJumped && animatorStateInfo.normalizedTime >= jumpTiming)
            {
                characterStateBase._GetCharacterControl(animator)._GetRigidBody.AddForce(Vector3.up * jumpForce);
                isJumped = true;
            }
            control._PullMultiplier = pull.Evaluate(animatorStateInfo.normalizedTime);
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);
            control._PullMultiplier = 0f;
            isJumped = false;
        }
    }
}


