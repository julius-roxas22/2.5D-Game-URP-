using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class PlayGameButton : MonoBehaviour
    {
        public void _OnClickButtonPlayGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_IndieGamePracticeScenes.DayScene.ToString());
        }
    }
}