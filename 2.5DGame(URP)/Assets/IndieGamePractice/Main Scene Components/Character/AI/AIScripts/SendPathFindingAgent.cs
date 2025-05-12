using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGamePractice
{

    public enum AITransitions
    {
        start_walk,
        jump_platform,
    }

    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/AI_Ability/SendPathFindingAgent")]
    public class SendPathFindingAgent : StateData
    {
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);
            AIProgress ai = control._GetAiProgress;

            if (null == control._GetAiProgress.agent)
            {
                GameObject obj = Instantiate(Resources.Load("PathFindingAgent", typeof(GameObject))) as GameObject;
                ai.agent = obj.GetComponent<PathFindingAgent>();
            }

            ai.agent.GetComponent<NavMeshAgent>().enabled = false;
            ai.agent.transform.position = control.transform.position;
            ai.agent._GotoTarget();
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);
            if (control._GetAiProgress.agent._StartWalk)
            {
                animator.SetBool(AITransitions.start_walk.ToString(), true);
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(AITransitions.start_walk.ToString(), false);
        }
    }
}


