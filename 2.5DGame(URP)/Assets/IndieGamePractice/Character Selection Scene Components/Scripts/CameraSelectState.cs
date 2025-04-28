using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace IndieGamePractice
{

    public class CameraSelectState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            _PlayableCharacterType[] arr = System.Enum.GetValues(typeof(_PlayableCharacterType)) as _PlayableCharacterType[];

            foreach (_PlayableCharacterType type in arr)
            {
                animator.SetBool(type.ToString(), false);
            }
        }
    }
}


