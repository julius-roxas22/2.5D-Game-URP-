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
                if (characterSelectData._CharacterSelectType != PlayableCharacterType.NONE)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(IndieGamePracticeScenes.MainScene.ToString());
                }
                else
                {
                    Debug.Log("Pick you`re character");
                }
            }
        }
    }
}


