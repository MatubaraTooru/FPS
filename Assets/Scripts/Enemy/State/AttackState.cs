

public class AttackState : IState
{
    EnemyController _enemyController;

    public AttackState (EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Enter()
    {
        _enemyController.Agent.destination = _enemyController.TargetDetector.TargetTransform.position;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {

    }

    void Shot()
    {

    }
}
