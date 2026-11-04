using UnityEngine;
using System;

public class SharkController : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[Header("Shark Settings")]
	[SerializeField] private float _sharkSpeed = 5f;
	[SerializeField] private float _chaseDistance = 60f;
	[SerializeField] private float _attackDistance = 8f;

	private bool _isChasing = false;
	private bool _isAttacking = false;
	private GameObject _raft;
	#endregion

	#region Unity Callbacks
	void Awake()
	{
		_raft = GameObject.FindGameObjectWithTag("Raft");

	}

	void Update()
	{
		if (Vector3.Distance(transform.position, _raft.transform.position) < _chaseDistance
			&& Vector3.Distance(transform.position, _raft.transform.position) > _attackDistance)
		{
			Chase();
		}
		else if (Vector3.Distance(transform.position, _raft.transform.position) < _attackDistance)
		{
			Attack();
		}
		else
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
		Debug.Log("Moving Shark");
	}

	private void Chase()
	{
		_isChasing = true;
		_sharkSpeed += 0.1f * Time.deltaTime;
		Debug.Log("Shark chasing");
	}

	private void Attack()
	{
		_isAttacking = true;
		Debug.Log("Shark Attacking");
	}


	#endregion

}