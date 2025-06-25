using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class ChangeScene : MonoBehaviour
    {
        public string _NextScene;

        public void _ChangeScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_NextScene);
        }
    }
}