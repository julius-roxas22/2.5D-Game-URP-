using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace IndieGamePractice
{
    public class CharacterStateBase : StateMachineBehaviour
    {
        private CharacterControl characterControl;
        [SerializeField] private List<StateData> allAbilitiesData = new List<StateData>();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in allAbilitiesData)
            {
                d._OnEnterAbility(this, animator, stateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in allAbilitiesData)
            {
                d._OnUpdateAbility(this, animator, stateInfo);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in allAbilitiesData)
            {
                d._OnExitAbility(this, animator, stateInfo);
            }
        }

        public CharacterControl _GetCharacterControl(Animator animator)
        {
            if (null == characterControl)
            {
                characterControl = animator.GetComponentInParent<CharacterControl>();
            }
            return characterControl;
        }
    }
}

