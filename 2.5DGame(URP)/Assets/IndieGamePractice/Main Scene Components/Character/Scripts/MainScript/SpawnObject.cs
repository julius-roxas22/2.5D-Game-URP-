using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/SpawnObject")]
    public class SpawnObject : StateData
    {
        [Range(0f, 1f)]
        [SerializeField] private float spawnTiming;
        [SerializeField] private PoolObjectType objType;
        [SerializeField] private string parentObjSpawned;
        [SerializeField] private bool stickToParent;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (spawnTiming == 0)
            {
                CharacterControl control = characterStateBase._CharacterControl;
                spawnObj(control);
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            if (!control._GetAnimationProgress._PoolObjectTypeList.Contains(objType))
            {
                if (animatorStateInfo.normalizedTime >= spawnTiming)
                {
                    spawnObj(control);
                }
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            if (control._GetAnimationProgress._PoolObjectTypeList.Contains(objType))
            {
                control._GetAnimationProgress._PoolObjectTypeList.Remove(objType);
            }
        }

        private void spawnObj(CharacterControl control)
        {
            if (control._GetAnimationProgress._PoolObjectTypeList.Contains(objType))
            {
                return;
            }

            GameObject obj = PoolManager._GetInstance._InstantiateObject(objType);

            if (!string.IsNullOrEmpty(parentObjSpawned))
            {
                GameObject p = control._GetChildObject(parentObjSpawned);
                obj.transform.parent = p.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
            }

            if (!stickToParent)
            {
                obj.transform.parent = null;
            }

            obj.SetActive(true);

            control._GetAnimationProgress._PoolObjectTypeList.Add(objType);
        }
    }
}


