using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class ManualInput : MonoBehaviour
    {
        private CharacterControl control;
        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        void Update()
        {
            control._MoveUp = VirtualInputManager._GetInstance._MoveUp;
            control._MoveDown = VirtualInputManager._GetInstance._MoveDown;
            control._MoveRight = VirtualInputManager._GetInstance._MoveRight;
            control._MoveLeft = VirtualInputManager._GetInstance._MoveLeft;
            control._Jump = VirtualInputManager._GetInstance._Jump;
            control._Attack = VirtualInputManager._GetInstance._Attack;
            control._Turbo = VirtualInputManager._GetInstance._Turbo;
        }
    }

}
