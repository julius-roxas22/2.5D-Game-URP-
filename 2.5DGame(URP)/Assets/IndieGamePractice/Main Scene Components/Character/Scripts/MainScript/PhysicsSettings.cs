using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Settings", menuName = "IndieGamePractice/Settings/PhysicsSettings")]
    public class PhysicsSettings : ScriptableObject
    {
        public int _DefaultSolverVelocityIterations;
    }
}