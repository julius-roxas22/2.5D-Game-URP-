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
        public PlayableCharacterType _PlayableCharacter;
        private CharacterHoverLight hoverLight;
        private CharacterSelectLight selectLight;
        private GameObject whiteSeletion;

        private CharacterSelectCamController camController;

        private void Awake()
        {
            _CharacterSelectData._CharacterSelectType = PlayableCharacterType.NONE;
            hoverLight = FindObjectOfType<CharacterHoverLight>();
            selectLight = FindObjectOfType<CharacterSelectLight>();

            whiteSeletion = GameObject.Find("WhiteSelection");
            whiteSeletion.SetActive(false);

            camController = FindAnyObjectByType<CharacterSelectCamController>();
        }

        private void Update()
        {
            ray = CameraManager._GetInstance._GetCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                CharacterControl control = hit.collider.gameObject.GetComponent<CharacterControl>();
                if (null != control)
                {
                    _PlayableCharacter = control.characterType;
                }
                else
                {
                    _PlayableCharacter = PlayableCharacterType.NONE;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (_PlayableCharacter != PlayableCharacterType.NONE)
                {
                    _CharacterSelectData._CharacterSelectType = _PlayableCharacter;
                    selectLight._GetSelectedLight.enabled = true;
                    selectLight.transform.position = hoverLight.transform.position;

                    CharacterControl control = CharacterManager._GetInstance._GetPlayableCharacters(_PlayableCharacter);

                    selectLight.transform.parent = control._SkinnedMesh.transform;

                    whiteSeletion.SetActive(true);
                    whiteSeletion.transform.parent = control._SkinnedMesh.transform;
                    whiteSeletion.transform.localPosition = new Vector3(0f, -0.05f, 0f);
                }
                else
                {
                    _CharacterSelectData._CharacterSelectType = PlayableCharacterType.NONE;
                    selectLight._GetSelectedLight.enabled = false;
                    whiteSeletion.SetActive(false);
                }

                foreach (CharacterControl control in CharacterManager._GetInstance._AllCharacters)
                {
                    if (control.characterType == _CharacterSelectData._CharacterSelectType)
                    {
                        control._SkinnedMesh.SetBool(TransitionParameters.ClickAnimation.ToString(), true);
                    }
                    else
                    {
                        control._SkinnedMesh.SetBool(TransitionParameters.ClickAnimation.ToString(), false);
                    }
                }
                camController._GetAnimator.SetBool(_CharacterSelectData._CharacterSelectType.ToString(), true);
            }
        }
    }
}