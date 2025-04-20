using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/Jump")]
    public class Jump : StateData
    {
        [SerializeField] private float jumpForce;
        [SerializeField] private AnimationCurve gravity;
        [SerializeField] private AnimationCurve pull;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            characterStateBase._GetCharacterControl(animator)._GetRigidBody.AddForce(Vector3.up * jumpForce);
            animator.SetBool(TransitionParameters.Grounded.ToString(), false);
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);
            control._GravityMultiplier = gravity.Evaluate(animatorStateInfo.normalizedTime);
            control._PullMultiplier = pull.Evaluate(animatorStateInfo.normalizedTime);
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


