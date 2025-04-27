using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool _MoveRight;
        public bool _MoveLeft;
        public bool _Jump;
        public bool _Attack;
    }
}
