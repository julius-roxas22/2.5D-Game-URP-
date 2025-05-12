using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/AI_Ability/JumpPlatform")]
    public class JumpPlatform : StateData
    {
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);
            control._Jump = true;
            control._MoveUp = true;

            if (control._GetAiProgress.agent._StartSphere.transform.position.z < control._GetAiProgress.agent._EndSphere.transform.position.z)
            {
                control._FaceForward(true);
            }
            else
            {
                control._FaceForward(false);
            }

        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);

            float topDist = control._GetAiProgress.agent._EndSphere.transform.position.y - control._FrontSpheres[1].transform.position.y;

            float bottomDist = control._GetAiProgress.agent._EndSphere.transform.position.y - control._BottomSpheres[0].transform.position.y;

            if (topDist < 3f && bottomDist > 0.55f)
            {
                if (control._IsFacingForward())
                {
                    control._MoveRight = true;
                    control._MoveLeft = false;
                }
                else
                {
                    control._MoveRight = false;
                    control._MoveLeft = true;
                }
            }

            if (bottomDist < 0.55f)
            {
                control._MoveRight = false;
                control._MoveLeft = false;
                control._Jump = false;
                control._MoveUp = false;
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


