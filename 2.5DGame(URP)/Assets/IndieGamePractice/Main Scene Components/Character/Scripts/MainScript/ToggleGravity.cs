using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/ToggleGravity")]
    public class ToggleGravity : StateData
    {
        [SerializeField] private bool onEnabled;
        [SerializeField] private bool onStart;
        [SerializeField] private bool onExit;
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (onStart)
            {
                CharacterControl control = characterStateBase._GetCharacterControl(animator);
                toggleGravity(control);
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
                toggleGravity(control);
            }
        }

        private void toggleGravity(CharacterControl control)
        {
            control._GetRigidBody.velocity = Vector3.zero;
            control._GetRigidBody.useGravity = onEnabled;
        }
    }
}


