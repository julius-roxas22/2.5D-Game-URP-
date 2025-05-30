using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
        {
            VirtualInputManager._GetInstance._MoveUp = Input.GetKey(KeyCode.W) ? true : false;
            VirtualInputManager._GetInstance._MoveDown = Input.GetKey(KeyCode.S) ? true : false;
            VirtualInputManager._GetInstance._MoveRight = Input.GetKey(KeyCode.D) ? true : false;
            VirtualInputManager._GetInstance._MoveLeft = Input.GetKey(KeyCode.A) ? true : false;
            VirtualInputManager._GetInstance._Jump = Input.GetKey(KeyCode.Space) ? true : false;
            VirtualInputManager._GetInstance._Attack = Input.GetKey(KeyCode.Return) ? true : false;
            VirtualInputManager._GetInstance._Turbo = Input.GetKey(KeyCode.LeftShift) ? true : false;
        }
    }
}

