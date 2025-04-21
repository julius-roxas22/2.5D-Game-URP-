using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class CameraController : MonoBehaviour
    {
        private Animator animator;

        public Animator _GetAnimator
        {
            get
            {
                if (null == animator)
                {
                    animator = GetComponent<Animator>();
                }
                return animator;
            }
        }

        public void _CameraTrigger(CameraType camType)
        {
            _GetAnimator.SetTrigger(camType.ToString());
        }
    }
}