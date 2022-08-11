using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    public IdleState(GameObject obj, NavMeshAgent agent, Animator animator, Transform targetTransform) : base(obj, agent, animator, targetTransform)
    {

    }

    public override void Enter()
    {
        animator.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        // 기본 상태일 때의 무언가를 해준다.

        if(Random.Range(0,100) < 30)
        {
            //nextState = new PatrolState(obj, agent, animator, playerTransform);
            curEvent = eEvent.EXIT;
        }
    }

    public override void Exit()
    {
        animator.SetTrigger("isIdle");
        base.Exit();
    }
}
