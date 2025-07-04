using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/ShakeCamera")]
    public class ShakeCamera : StateData
    {
        [Range(0, 1f)]
        [SerializeField] private float shakeTiming;
        private bool isShaking;
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            if (shakeTiming == 0)
            {
                CameraManager._GetInstance._ShakeCamera(0.45f);
                control._GetAnimationProgress._IsCameraShaken = true;
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            if (!control._GetAnimationProgress._IsCameraShaken && animatorStateInfo.normalizedTime >= shakeTiming)
            {
                CameraManager._GetInstance._ShakeCamera(0.45f);
                control._GetAnimationProgress._IsCameraShaken = true;
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            control._GetAnimationProgress._IsCameraShaken = false;
        }
    }
}


