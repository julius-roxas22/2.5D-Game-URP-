using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class AttackManager : Singleton<AttackManager>
    {
        public List<AttackInfo> _CurrentAttacks = new List<AttackInfo>();
    }

}
