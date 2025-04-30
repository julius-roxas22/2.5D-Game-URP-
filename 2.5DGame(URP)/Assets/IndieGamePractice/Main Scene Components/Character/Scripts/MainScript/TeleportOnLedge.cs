using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/TeleportOnLedge")]
    public class TeleportOnLedge : StateData
    {
        [SerializeField] private Vector3 offset;
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = CharacterManager._GetInstance._GetPlayableCharacters(animator);
            control.transform.position = control._GetLedgeChecker._Ledge.transform.position + control._GetLedgeChecker._Ledge.transform.TransformDirection(offset);
            control._SkinnedMesh.transform.position = control._GetLedgeChecker._Ledge.transform.position + control._GetLedgeChecker._Ledge.transform.TransformDirection(offset);
            control._SkinnedMesh.transform.parent = control.transform;
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


