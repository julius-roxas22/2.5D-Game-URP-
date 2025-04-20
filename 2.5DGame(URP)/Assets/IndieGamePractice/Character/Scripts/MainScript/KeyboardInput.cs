using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
        {
            VirtualInputManager._GetInstance._MoveRight = Input.GetKey(KeyCode.D) ? true : false;
            VirtualInputManager._GetInstance._MoveLeft = Input.GetKey(KeyCode.A) ? true : false;
            VirtualInputManager._GetInstance._Jump = Input.GetKey(KeyCode.Space) ? true : false;
            VirtualInputManager._GetInstance._Attack = Input.GetKey(KeyCode.Return) ? true : false;
        }
    }
}

