using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class CameraStateBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CameraType[] arr = System.Enum.GetValues(typeof(CameraType)) as CameraType[];
            foreach (CameraType type in arr)
            {
                animator.ResetTrigger(type.ToString());
            }
        }
    }
}