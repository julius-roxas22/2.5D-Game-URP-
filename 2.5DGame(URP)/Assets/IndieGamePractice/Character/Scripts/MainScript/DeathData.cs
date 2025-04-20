using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Death Data", menuName = "IndieGamePractice/Create/Death/DeathData")]
    public class DeathData : ScriptableObject
    {
        public List<BodyPart> _DamagedParts = new List<BodyPart>();
        public RuntimeAnimatorController _DeathAnimationController;
    }

}
