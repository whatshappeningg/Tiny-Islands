using UnityEngine;
using System;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class SharkController : MonoBehaviour
{
	#region Properties
	public event Action<float> OnAttack;

	#endregion

	#region Fields
	[Header("Shark Movement Settings")]
	[SerializeField] private float _sharkSpeed = 9f;
	[SerializeField] private float _chaseDistance = 60f;
	[SerializeField] private float _attackDistance = 10f;
	[SerializeField] private float _movementRadius = 200f;

	[Header("Shark Attack Settings")]
	[SerializeField] private float _attackDamage = 5f;
	[SerializeField] private float _attackCooldown = 2f;
	private float _currentAttackCooldown = 0f;

	private bool _isChasing = false;
	private bool _isAttacking = false;
	private GameObject _raft;
	private Animator _animator;
	private NavMeshAgent _agent;

	#endregion

	#region Unity Callbacks
	void Awake()
	{
		_raft = GameObject.FindGameObjectWithTag("Raft");
		_animator = GetComponentInChildren<Animator>();
		_agent = GetComponent<NavMeshAgent>();
	}

	void Start()
	{
		_agent.speed = _sharkSpeed;
	}

	void Update()
	{
		if (Vector3.Distance(transform.position, _raft.transform.position) < _chaseDistance
			&& Vector3.Distance(transform.position, _raft.transform.position) > _attackDistance)
		{
			ChaseMode();
		}
		if (Vector3.Distance(transform.position, _raft.transform.position) < _attackDistance)
		{
			AttackMode();
		}
		if (Vector3.Distance(transform.position, _raft.transform.position) > _chaseDistance)
		{
			MovingMode();
		}

	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _chaseDistance);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _attackDistance);
	}

	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void MovingMode()
	{
		_isChasing = false;
		_isAttacking = false;

		//_animator.SetBool("Attacking", false);

		_agent.speed = _sharkSpeed;

		if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
		{
			_agent.SetDestination(FindRandomDestination(_movementRadius));
		}

		Debug.Log("Moving Shark");
	}

	private void ChaseMode()
	{
		_isChasing = true;

		_animator.SetBool("Attacking", false);

		_agent.destination = _raft.transform.position;
		_agent.speed += 0.2f * Time.deltaTime;
		_agent.speed = Mathf.Clamp(_agent.speed, _sharkSpeed, 10f);

		Debug.Log("Shark chasing");
	}

	private void AttackMode()
	{
		_isAttacking = true;

		_animator.SetBool("Attacking", true);
		_agent.speed = 0f;

		Attack();

		Debug.Log("Shark Attacking");
	}

	private void Attack()
	{
		_currentAttackCooldown += Time.deltaTime;
		if (_currentAttackCooldown >= _attackCooldown)
		{
			_currentAttackCooldown = 0f;
			OnAttack?.Invoke(_attackDamage);
		}
	}

	private Vector3 FindRandomDestination(float radius)
	{
		Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * radius;
		randomPoint += transform.position;

		NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, radius, 1);
		Vector3 finalPosition = hit.position;

		return finalPosition;
	}

	#endregion

}