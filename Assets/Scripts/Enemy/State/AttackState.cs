

using System;
using UnityEngine;

public class AttackState : IState
{
    EnemyController _enemyController;
    float _timer;

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
        _timer += Time.deltaTime;

        if (_enemyController.Agent.stoppingDistance < (_enemyController.TargetDetector.TargetTransform.position - _enemyController.transform.position).sqrMagnitude * 2)
        {
            _enemyController.StateMachine.TransitionTo(_enemyController.StateMachine.MoveState);
        }

        if (_timer > 60f / _enemyController.Weapon.FireRate)
        {
            Shot();
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {

    }

    void Shot()
    {
        _enemyController.transform.forward = (_enemyController.TargetDetector.TargetTransform.position - _enemyController.transform.position) + Vector3.up * 0;
        var x = UnityEngine.Random.value;
        var y = UnityEngine.Random.value;
        Ray ray = new Ray(_enemyController.Muzzle.position, _enemyController.transform.forward + new Vector3(x, y, 0));
        Debug.DrawRay(_enemyController.Muzzle.position, _enemyController.transform.forward * _enemyController.Weapon.Range, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, _enemyController.Weapon.Range))
        {
            if (hit.collider.TryGetComponent(out HPManager hpManager) && hit.collider.CompareTag("Enemy"))
            {
                var dis = (hit.point - _enemyController.Muzzle.position).sqrMagnitude * 2;
                var damage = (int)Math.Floor((1 - dis / _enemyController.Weapon.Range) * _enemyController.Weapon.MaxDamage);
                hpManager.GetDamage(damage);
                Debug.Log(damage);
            }
        }

        _timer = 0;
    }
}
