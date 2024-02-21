using System;
using Unity.VisualScripting;

[Serializable]
public class EnemyStateMachine
{
    public IState CurrentState { get; private set; }

    public IdleState IdleState;
    public MoveState MoveState;
    public AttackState AttackState;
    public DeadState DeadState;

    public EnemyStateMachine (EnemyController controller)
    {
        IdleState = new IdleState(controller);
        MoveState = new MoveState(controller);
        AttackState = new AttackState(controller);
        DeadState = new DeadState(controller);
    }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }

    public void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.FixedUpdate();
        }
    }
}
