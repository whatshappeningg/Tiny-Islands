using UnityEngine;
using System;

public class RaftSensors : MonoBehaviour
{
	#region Properties
	public bool IsPlayerInAccessArea => isPlayerInAccessArea;

	#endregion

	#region Fields
	private readonly float _accessAreaRadius = 5f;
	[SerializeField] private bool isPlayerInAccessArea = false;


	#endregion

	#region Unity Callbacks
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerInAccessArea = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerInAccessArea = false;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, _accessAreaRadius);

	}

	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion

}