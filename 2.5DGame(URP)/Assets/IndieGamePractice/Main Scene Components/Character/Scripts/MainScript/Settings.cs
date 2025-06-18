using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private FrameSettings frameSettings;
        [SerializeField] private PhysicsSettings physicsSettings;

        private void Awake()
        {
            Time.timeScale = frameSettings._TimeScale;
            Application.targetFrameRate = frameSettings._TargetFps;
            Physics.defaultSolverVelocityIterations = physicsSettings._DefaultSolverVelocityIterations;
            VirtualInputManager._GetInstance._LoadKeys();
        }
    }
}


