using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class CharacterSelectCamController : MonoBehaviour
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
    }
}


