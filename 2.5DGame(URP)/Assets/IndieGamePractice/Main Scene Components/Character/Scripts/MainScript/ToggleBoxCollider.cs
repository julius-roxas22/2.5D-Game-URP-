using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/ToggleBoxCollider")]
    public class ToggleBoxCollider : StateData
    {
        [SerializeField] private bool onEnable;
        [SerializeField] private bool onStart;
        [SerializeField] private bool onExit;
        [Space(10)] [SerializeField] private bool onRepositionSpheres;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (onStart)
            {
                CharacterControl control = characterStateBase._CharacterControl;
                toggleBoxCollider(control);
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (onExit)
            {
                CharacterControl control = characterStateBase._CharacterControl;
                toggleBoxCollider(control);
            }
        }

        private void toggleBoxCollider(CharacterControl control)
        {
            control.GetComponent<BoxCollider>().enabled = onEnable;
            if (onRepositionSpheres)
            {
                control._RepositionFrontSpheres();
                control._RepositionBottomSpheres();
            }
        }
    }
}


