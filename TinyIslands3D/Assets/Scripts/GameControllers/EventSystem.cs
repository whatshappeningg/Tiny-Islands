using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private RaftSensors _raft;
	[SerializeField] private SharkController _shark;

	#endregion

	#region Unity Callbacks
	void Start()
	{
		_shark.OnAttack += OnTakeDamage;

	}

	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void OnTakeDamage(float damage)
	{
		_raft.TakeDamage(damage);
	}

	#endregion

}