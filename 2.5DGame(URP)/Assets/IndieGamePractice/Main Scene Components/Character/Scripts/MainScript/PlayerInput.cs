using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class PlayerInput : MonoBehaviour
    {
        public SavedKeys _SavedKeys;

        void Update()
        {
            VirtualInputManager._GetInstance._MoveUp = Input.GetKey(VirtualInputManager._GetInstance._DictionaryKeys[InputKeyType.UP]) ? true : false;
            VirtualInputManager._GetInstance._MoveDown = Input.GetKey(VirtualInputManager._GetInstance._DictionaryKeys[InputKeyType.DOWN]) ? true : false;
            VirtualInputManager._GetInstance._MoveRight = Input.GetKey(VirtualInputManager._GetInstance._DictionaryKeys[InputKeyType.RIGHT]) ? true : false;
            VirtualInputManager._GetInstance._MoveLeft = Input.GetKey(VirtualInputManager._GetInstance._DictionaryKeys[InputKeyType.LEFT]) ? true : false;
            VirtualInputManager._GetInstance._Jump = Input.GetKey(VirtualInputManager._GetInstance._DictionaryKeys[InputKeyType.JUMP]) ? true : false;
            VirtualInputManager._GetInstance._Attack = Input.GetKey(VirtualInputManager._GetInstance._DictionaryKeys[InputKeyType.ATTACK]) ? true : false;
            VirtualInputManager._GetInstance._Turbo = Input.GetKey(VirtualInputManager._GetInstance._DictionaryKeys[InputKeyType.TURBO]) ? true : false;
        }
    }
}

