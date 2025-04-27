using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/ResetLocalTransform")]
    public class ResetLocalTransform : StateData
    {
        [SerializeField] private bool onStart;
        [SerializeField] private bool onExit;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (onStart)
            {
                CharacterControl control = characterStateBase._GetCharacterControl(animator);
                control._SkinnedMesh.transform.localPosition = Vector3.zero;
                control._SkinnedMesh.transform.localRotation = Quaternion.identity;
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (onExit)
            {
                CharacterControl control = characterStateBase._GetCharacterControl(animator);
                control._SkinnedMesh.transform.localPosition = Vector3.zero;
                control._SkinnedMesh.transform.localRotation = Quaternion.identity;
            }
        }
    }
}


