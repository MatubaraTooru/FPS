using UnityEngine;

public class IdleState : IState
{
    private EnemyController _enemyController;

    public IdleState (EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
        if (_enemyController.TargetDetector.TargetTransform != null || _enemyController.Agent.destination != null)
        {
            _enemyController.StateMachine.TransitionTo(_enemyController.StateMachine.MoveState);
        }

        if (_enemyController.TargetDetector.IsTargetDetected)
        {
            _enemyController.StateMachine.TransitionTo (_enemyController.StateMachine.AttackState);
        }
        Debug.Log("Updating Idle State");
    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {

    }
}
