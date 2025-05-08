using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class PlayGame : MonoBehaviour
    {
        [SerializeField] private CharacterSelectData characterSelectData;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (characterSelectData._CharacterSelectType != _PlayableCharacterType.NONE)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(_IndieGamePracticeScenes.MainScene.ToString());
                }
                else
                {
                    Debug.Log("Choose your hero!");
                }
            }
        }
    }
}


