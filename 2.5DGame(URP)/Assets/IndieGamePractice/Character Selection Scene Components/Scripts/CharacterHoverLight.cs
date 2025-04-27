using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class CharacterHoverLight : MonoBehaviour
    {

        [SerializeField] private Vector3 offset;
        private CharacterControl hoverSelectedCharacter;
        private MouseControl mouseControl;
        private Light hoverLight;

        private void Awake()
        {
            mouseControl = FindObjectOfType<MouseControl>();
            hoverLight = GetComponent<Light>();
        }

        private void Update()
        {
            if (mouseControl._HoverCharacterType == PlayableCharacterType.NONE)
            {
                hoverSelectedCharacter = null;
                hoverLight.enabled = false;
            }
            else
            {
                hoverLight.enabled = true;
                lightUpSelectedCharacter();
            }
        }

        private void lightUpSelectedCharacter()
        {
            if (null == hoverSelectedCharacter)
            {
                hoverSelectedCharacter = CharacterManager._GetInstance._GetPlayableCharacters(mouseControl._HoverCharacterType);
                transform.position = hoverSelectedCharacter.transform.position + hoverSelectedCharacter.transform.TransformDirection(offset);
            }
        }
    }
}

