using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Death/RagdollDeath")]
    public class RagdollDeathTrigger : StateData
    {

        [SerializeField] private float ragdollTiming;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            if (animatorStateInfo.normalizedTime >= ragdollTiming)
            {
                if (!control._GetAnimationProgress._RagdollTriggered)
                {
                    if (control._SkinnedMesh.enabled)
                    {
                        control._GetAnimationProgress._RagdollTriggered = true;
                    }
                }
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


