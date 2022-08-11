using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    // 가질 수 있는 상태
    public enum eState
    {
        IDLE,
        PATROL,
        PURSUE,
        ATTACK,
        SLEEP,
        RUNAWAY
    }

    public enum eEvent
    {
        ENTER,
        UPDATE,
        EXIT
    }

    public eState state;
    protected eEvent curEvent;

    protected GameObject obj;
    protected NavMeshAgent agent;

    protected Animator animator;
    protected Transform playerTransform;

    protected State nextState;

    private float detectDistance = 10f;
    private float detectAngle = 30f;
    private float shootDistance = 7f;

    public State(GameObject obj, NavMeshAgent agent, Animator animator, Transform targetTransform)
    {
        this.obj = obj;
        this.agent = agent;
        this.animator = animator;
        playerTransform = targetTransform;

        curEvent = eEvent.ENTER;
    }

    public virtual void Enter()
    {
        curEvent = eEvent.UPDATE;
    }

    public virtual void Update()
    {
        curEvent = eEvent.UPDATE;
    }

    public virtual void Exit()
    {
        curEvent = eEvent.EXIT;
    }

    public State Process()
    {
        if (curEvent == eEvent.ENTER) Enter();
        if (curEvent == eEvent.UPDATE) Update();
        if (curEvent == eEvent.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }
}
