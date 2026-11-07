using UnityEngine;
using System;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class SharkController : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[Header("Shark Settings")]
	[SerializeField] private float _sharkSpeed = 9f;
	[SerializeField] private float _chaseDistance = 60f;
	[SerializeField] private float _attackDistance = 8f;
	[SerializeField] private float _movementRadius = 200f;


	private bool _isChasing = false;
	private bool _isAttacking = false;
	private GameObject _raft;
	private NavMeshAgent _agent;

	#endregion

	#region Unity Callbacks
	void Awake()
	{
		_raft = GameObject.FindGameObjectWithTag("Raft");
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
			Chase();
		}
		if (Vector3.Distance(transform.position, _raft.transform.position) < _attackDistance)
		{
			Attack();
		}
		if (Vector3.Distance(transform.position, _raft.transform.position) > _chaseDistance)
		{
			Move();
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
	private void Move()
	{
		_isChasing = false;
		_isAttacking = false;

		_agent.speed = _sharkSpeed;

		if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
		{
			_agent.SetDestination(FindRandomDestination(_movementRadius));
		}

		Debug.Log("Moving Shark");
	}

	private void Chase()
	{
		_isChasing = true;

		_agent.destination = _raft.transform.position;
		_agent.speed += 0.2f * Time.deltaTime;
		_agent.speed = Mathf.Clamp(_agent.speed, _sharkSpeed, 10f);

		Debug.Log("Shark chasing");
	}

	private void Attack()
	{
		_isAttacking = true;
		_agent.speed = 0f;

		//_agent.isStopped = true;

		Debug.Log("Shark Attacking");
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