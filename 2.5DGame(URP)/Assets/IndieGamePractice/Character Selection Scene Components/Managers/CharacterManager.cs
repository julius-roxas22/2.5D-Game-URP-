using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        public List<CharacterControl> _AllCharacters = new List<CharacterControl>();

        public CharacterControl _GetPlayableCharacters(_PlayableCharacterType playableCharacterType)
        {
            foreach (CharacterControl control in _AllCharacters)
            {
                if (control.characterType == playableCharacterType)
                {
                    return control;
                }
            }
            return null;
        }

        public CharacterControl _GetPlayableCharacters(Animator skinnedMesh)
        {
            foreach (CharacterControl control in _AllCharacters)
            {
                if (control._SkinnedMesh == skinnedMesh)
                {
                    return control;
                }
            }
            return null;
        }

        public CharacterControl _GetPlayableCharacters()
        {
            foreach (CharacterControl control in _AllCharacters)
            {
                ManualInput manualInput = control.GetComponent<ManualInput>();
                if (null != manualInput)
                {
                    if (manualInput.enabled)
                    {
                        return control;
                    }
                }
            }
            return null;
        }
    }
}

