using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/AI_Ability/FallPlatform")]
    public class FallPlatform : StateData
    {
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;

            if (control._GetAiProgress.agent._EndSphere.transform.position.z > control.transform.position.z)
            {
                control._FaceForward(true);
            }
            else if (control._GetAiProgress.agent._EndSphere.transform.position.z < control.transform.position.z)
            {
                control._FaceForward(false);
            }

            if (control._GetAiProgress._GetDistanceToStartSphere() > 3f)
            {
                control._Turbo = true;
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;

            if (!control._SkinnedMesh.GetBool(_TransitionParameters.Grounded.ToString()))
            {
                return;
            }

            if (control._IsFacingForward())
            {
                if (control._GetAiProgress.agent._EndSphere.transform.position.z > control.transform.position.z)
                {
                    control._MoveRight = true;
                    control._MoveLeft = false;
                }
                else
                {
                    control._MoveRight = false;
                    control._MoveLeft = false;

                    control._GetAiController._InitializeAI();
                }
            }
            else
            {
                if (control._GetAiProgress.agent._EndSphere.transform.position.z < control.transform.position.z)
                {
                    control._MoveRight = false;
                    control._MoveLeft = true;
                }
                else
                {
                    control._MoveRight = false;
                    control._MoveLeft = false;

                    control._GetAiController._InitializeAI();
                }
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


