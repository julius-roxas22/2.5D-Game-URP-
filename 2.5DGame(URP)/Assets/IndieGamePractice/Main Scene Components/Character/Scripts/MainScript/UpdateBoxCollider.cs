using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/UpdateBoxCollider")]
    public class UpdateBoxCollider : StateData
    {
        [SerializeField] private float targetCenterSpeed;
        [SerializeField] private Vector3 targetCenter;

        [SerializeField] private float targetSizeSpeed;
        [SerializeField] private Vector3 targetSize;

        [Space(10)]
        [SerializeField] private bool keepUpdating;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            control._GetAnimationProgress._UpdatingBoxCollider = true;

            control._GetAnimationProgress._CenterSpeed = targetCenterSpeed;
            control._GetAnimationProgress._TargetCenter = targetCenter;

            control._GetAnimationProgress._SizeSpeed = targetSizeSpeed;
            control._GetAnimationProgress._TargetSize = targetSize;
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            if (!keepUpdating)
            {
                control._GetAnimationProgress._UpdatingBoxCollider = false;
            }
        }
    }
}


