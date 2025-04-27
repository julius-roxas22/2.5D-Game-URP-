using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class PlayerSpawn : MonoBehaviour
    {
        [SerializeField] private CharacterSelectData characterSelectData;
        private string objName;

        private void Awake()
        {
            switch (characterSelectData._CharacterSelectType)
            {
                case PlayableCharacterType.YELLOW:
                    {
                        objName = "YBot - Yellow";
                        break;
                    }

                case PlayableCharacterType.RED:
                    {
                        objName = "YBot - Red";
                        break;
                    }
                case PlayableCharacterType.BLUE:
                    {
                        objName = "YBot - Blue";
                        break;
                    }
            }

            GameObject obj = Instantiate(Resources.Load(objName, typeof(GameObject))) as GameObject;
            obj.transform.position = transform.position;
            GetComponent<MeshRenderer>().enabled = false;

            Cinemachine.CinemachineVirtualCamera[] allCamera = FindObjectsOfType<Cinemachine.CinemachineVirtualCamera>();
            CharacterControl control = obj.GetComponent<CharacterControl>();

            if (!control.GetComponent<ManualInput>().enabled)
            {
                control.GetComponent<ManualInput>().enabled = true;
            }

            foreach (Cinemachine.CinemachineVirtualCamera c in allCamera)
            {
                Transform bodyTransform = control._GetBodyPartTransform("Spine1");

                c.LookAt = bodyTransform;
                c.Follow = bodyTransform;
            }
        }
    }
}


