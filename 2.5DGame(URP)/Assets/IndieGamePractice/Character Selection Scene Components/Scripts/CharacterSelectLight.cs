using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class CharacterSelectLight : MonoBehaviour
    {
        private Light selectedLight;

        public Light _GetSelectedLight
        {
            get
            {
                if (null == selectedLight)
                {
                    selectedLight = GetComponent<Light>();
                }
                return selectedLight;
            }
        }

        private void Awake()
        {
            _GetSelectedLight.enabled = false;
        }
    }
}

