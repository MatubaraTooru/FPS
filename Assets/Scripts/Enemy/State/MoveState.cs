using UnityEngine;

public class MoveState : IState
{
    EnemyController _enemyController;

    public MoveState (EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Enter()
    {
        MoveToTarget();
    }

    public void Update()
    {
        if (_enemyController.TargetDetector.IsTargetDetected)
        {
            _enemyController.StateMachine.TransitionTo(_enemyController.StateMachine.AttackState);
        }
        else if (_enemyController.Agent.velocity == Vector3.zero)
        {
            _enemyController.StateMachine.TransitionTo(_enemyController.StateMachine.IdleState);
        }

        if (_enemyController.Agent.velocity.magnitude > 0)
        {
            _enemyController.Animator.SetBool("IsMoving", true);
        }
        else
        {
            _enemyController.Animator.SetBool("IsMoving", false);
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {

    }

    private void MoveToTarget()
    {
        _enemyController.Agent.destination = _enemyController.TargetDetector.TargetTransform.position;
    }
}
