using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/AI_Ability/StartRunning")]
    public class StartRunning : StateData
    {
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);

            if (control._GetAiProgress.agent._StartSphere.transform.position.z > control.transform.position.z)
            {
                control._FaceForward(true);
                control._MoveRight = true;
                control._MoveLeft = false;
            }
            else
            {
                control._FaceForward(false);
                control._MoveRight = false;
                control._MoveLeft = true;
            }

            float dist = (control._GetAiProgress.agent._StartSphere.transform.position - control.transform.position).sqrMagnitude;
            if (dist > 2f)
            {
                control._Turbo = true;
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);

            float dist = (control._GetAiProgress.agent._StartSphere.transform.position - control.transform.position).sqrMagnitude;
            if (dist < 2f)
            {
                control._MoveRight = false;
                control._MoveLeft = false;
                control._Turbo = false;
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


