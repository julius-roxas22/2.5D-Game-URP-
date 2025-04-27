using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class MouseControl : MonoBehaviour
    {
        [SerializeField] private CharacterSelectData _CharacterSelectData;
        private Ray ray;
        private RaycastHit hit;
        public PlayableCharacterType _HoverCharacterType;
        private CharacterHoverLight hoverLight;
        private CharacterSelectLight selectLight;

        private void Awake()
        {
            _CharacterSelectData._CharacterSelectType = PlayableCharacterType.NONE;
            hoverLight = FindObjectOfType<CharacterHoverLight>();
            selectLight = FindObjectOfType<CharacterSelectLight>();
        }

        private void Update()
        {
            ray = CameraManager._GetInstance._GetCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                CharacterControl control = hit.collider.gameObject.GetComponent<CharacterControl>();
                if (null != control)
                {
                    _HoverCharacterType = control.characterType;
                }
                else
                {
                    _HoverCharacterType = PlayableCharacterType.NONE;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (_HoverCharacterType != PlayableCharacterType.NONE)
                {
                    _CharacterSelectData._CharacterSelectType = _HoverCharacterType;
                    selectLight._GetSelectedLight.enabled = true;
                    selectLight.transform.position = hoverLight.transform.position;
                }
                else
                {
                    _CharacterSelectData._CharacterSelectType = PlayableCharacterType.NONE;
                    selectLight._GetSelectedLight.enabled = false;
                }
            }
        }
    }
}