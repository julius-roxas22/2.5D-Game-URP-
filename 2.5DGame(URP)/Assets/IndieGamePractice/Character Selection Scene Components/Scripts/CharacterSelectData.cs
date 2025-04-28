using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{

    public enum _PlayableCharacterType
    {
        NONE,
        RED,
        YELLOW,
        BLUE
    }

    [CreateAssetMenu(fileName = "Character Select Data", menuName = "IndieGamePractice/CreateCharacterSelect/CharacterSelect")]
    public class CharacterSelectData : ScriptableObject
    {
        public _PlayableCharacterType _CharacterSelectType;
    }
}
