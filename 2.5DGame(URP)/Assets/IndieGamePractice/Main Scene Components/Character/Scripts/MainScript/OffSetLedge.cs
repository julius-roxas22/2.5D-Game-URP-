using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/OffSetLedge")]
    public class OffSetLedge : StateData
    {
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);
            GameObject anim = control._SkinnedMesh.gameObject;
            anim.transform.parent = control._GetLedgeChecker._Ledge.transform;
            anim.transform.localPosition = control._GetLedgeChecker._Ledge._Offset;
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


