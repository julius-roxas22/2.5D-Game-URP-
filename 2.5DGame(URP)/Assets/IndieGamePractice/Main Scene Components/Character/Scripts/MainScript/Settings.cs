using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private FrameSettings frameSettings;

        private void Awake()
        {
            Time.timeScale = frameSettings._TimeScale;
            Application.targetFrameRate = frameSettings._TargetFps;
        }
    }
}


