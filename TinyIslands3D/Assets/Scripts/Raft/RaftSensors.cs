using UnityEngine;
using System;

public class RaftSensors : MonoBehaviour, IInteractable
{
	#region Properties
	public event Action OnChangeGameMode;
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
	#endregion

	#region Unity Callbacks
	void Awake()
	{
		_currentHealth = _maxHealth;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, _accessAreaRadius);

	}

	#endregion

	#region Public Methods
	public void Interact()
	{
		OnChangeGameMode?.Invoke();
	}
	public InteractableInfo GetInfo()
	{
		InteractableInfo info = new()
		{
			Action = "enter",
			Type = "raft"
		};
		return info;
	}
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