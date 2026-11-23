using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private RaftSensors _raftSensors;
	[SerializeField] private CannonController _cannon;
	[SerializeField] private SharkController _shark;
	[SerializeField] private GameModeController _gameModeController;
	[SerializeField] private InteractionSystem _interactionSystem;
	[SerializeField] private Inventory _inventory;

	#endregion

	#region Unity Callbacks
	void Start()
	{
		_shark.OnAttack += OnTakeDamage;
		_interactionSystem.OnInteracted += OnInteracted;

	}

	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void OnTakeDamage(float damage)
	{
		_raftSensors.TakeDamage(damage);
	}
	private void OnInteracted(IInteractable target, InteractionOption option)
	{
		switch (option.Action)
		{
			case InteractionType.collect:
				OnCollect(target);
				break;
			case InteractionType.embark:
			case InteractionType.disembark:
				OnChangeGameMode();
				break;
			case InteractionType.repair:
				_raftSensors.Repair(5);
				break;
			case InteractionType.fire:
				_cannon.CanFire();
				break;
		}
	}
	private void OnCollect(IInteractable target)
	{
		if (target is Recolectable recolectable)
		{
			_inventory.AddRecolectable(recolectable.Type, recolectable.Count);
			Destroy(recolectable.gameObject);
		}
	}
	private void OnChangeGameMode()
	{
		_gameModeController.SwitchGameMode();
	}

	#endregion

}