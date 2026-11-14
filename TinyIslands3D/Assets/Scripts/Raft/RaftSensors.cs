using UnityEngine;
using System;

public class RaftSensors : MonoBehaviour
{
	#region Properties
	public bool IsPlayerInAccessArea => _isPlayerInAccessArea;
	public float CurrentHealth
	{
		get
		{
			return _currentHealth;
		}
		set
		{
			_currentHealth = value;

			if (value < 0)
			{
				_currentHealth = 0;
				Die();
			}

			if (value > _maxHealth)
				_currentHealth = _maxHealth;
		}
	}
	public event Action<float> OnHealthChanged;
	#endregion

	#region Fields
	private readonly float _accessAreaRadius = 5f;
	[SerializeField] private float _maxHealth = 100f;
	[SerializeField] private float _currentHealth;
	[SerializeField] private bool _isPlayerInAccessArea = false;

	#endregion

	#region Unity Callbacks
	void Awake()
	{
		_currentHealth = _maxHealth;
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_isPlayerInAccessArea = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_isPlayerInAccessArea = false;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, _accessAreaRadius);

	}

	#endregion

	#region Public Methods
	public void TakeDamage(float damage)
	{
		_currentHealth -= damage;
		OnHealthChanged?.Invoke(_currentHealth);
	}
	public void Repair(float amount)
	{
		_currentHealth += amount;
		OnHealthChanged?.Invoke(_currentHealth);
	}

	#endregion

	#region Private Methods
	private void Die()
	{
		Debug.Log("Raft destroyed!");
		// Add logic for raft destruction (e.g., game over, respawn, etc.)
	}
	#endregion

}