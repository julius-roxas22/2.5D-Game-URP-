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
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = CharacterManager._GetInstance._GetPlayableCharacters(animator);
            Ledge ledge = control._GetLedgeChecker._Ledge;
            control.transform.position = ledge.transform.position + ledge.transform.TransformDirection(ledge._EndPosition);
            control._SkinnedMesh.transform.position = control.transform.position;
            control._SkinnedMesh.transform.parent = control.transform;
        }
    }
}


