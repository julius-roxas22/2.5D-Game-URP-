using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{

    public enum PlayableCharacterType
    {
        NONE,
        RED,
        YELLOW,
        BLUE
    }

    [CreateAssetMenu(fileName = "Character Select Data", menuName = "IndieGamePractice/CreateCharacterSelect/CharacterSelect")]
    public class CharacterSelectData : ScriptableObject
    {
        public PlayableCharacterType _CharacterSelectType;
    }
}
