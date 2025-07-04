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
        fall_platform,
        start_running
    }

    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/AI_Ability/SendPathFindingAgent")]
    public class SendPathFindingAgent : StateData
    {
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;

            if (null == control._GetAiProgress.agent)
            {
                GameObject obj = Instantiate(Resources.Load("PathFindingAgent", typeof(GameObject))) as GameObject;
                control._GetAiProgress.agent = obj.GetComponent<PathFindingAgent>();
            }

            control._GetAiProgress.agent._Owner = control;
            control._GetAiProgress.agent.GetComponent<NavMeshAgent>().enabled = false;
            control._GetAiProgress.agent.transform.position = control.transform.position;
            control._GetNavMeshObstacle.carving = false;
            control._GetAiProgress.agent._GotoTarget();
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
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


