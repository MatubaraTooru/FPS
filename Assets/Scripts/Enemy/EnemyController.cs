using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    EnemyStateMachine _enemyStateMachine;

    NavMeshAgent _agent;
    [SerializeField] float _moveSpeed = 3.5f;
    [SerializeField] float _splintSpeed = 5f;
    [SerializeField] TargetDetector _targetDetector;
    [SerializeField] HPManager _hpManager;
    [SerializeField] Animator _animator;
    [SerializeField] Transform _muzzle;
    [SerializeField] Weapon _weaponData;

    public EnemyStateMachine StateMachine => _enemyStateMachine;
    public NavMeshAgent Agent => _agent;
    public HPManager HPManager => _hpManager;
    public TargetDetector TargetDetector => _targetDetector;
    public Animator Animator => _animator;
    public Transform Muzzle => _muzzle;
    public Weapon Weapon => _weaponData;

    private void Awake()
    {
        _enemyStateMachine = new(this);
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _moveSpeed;
    }
    void Start()
    {
        _enemyStateMachine.Initialize(_enemyStateMachine.IdleState);
    }

    void Update()
    {
        _enemyStateMachine.Update();
    }

    private void FixedUpdate()
    {
        _enemyStateMachine.FixedUpdate();
    }
}
