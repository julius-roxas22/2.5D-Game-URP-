using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum CameraType
    {
        Default,
        Shake
    }

    public class CameraManager : Singleton<CameraManager>
    {
        private Coroutine coroutine;
        private CameraController camController;
        private Camera cam;

        public Camera _GetCamera
        {
            get
            {
                if(null == cam)
                {
                    cam = FindObjectOfType<Camera>();
                }
                return cam;
            }
        }

        public CameraController _GetCamController
        {
            get
            {
                if (null == camController)
                {
                    camController = FindAnyObjectByType<CameraController>();
                }
                return camController;
            }
        }

        public void _ShakeCamera(float sec)
        {
            if (null != coroutine)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(cameraFadeOut(sec));
        }

        IEnumerator cameraFadeOut(float sec)
        {
            _GetCamController._CameraTrigger(CameraType.Shake);
            yield return new WaitForSeconds(sec);
            _GetCamController._CameraTrigger(CameraType.Default);
        }
    }
}

