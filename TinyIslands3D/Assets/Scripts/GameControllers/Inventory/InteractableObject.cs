using UnityEngine;
using System.Collections.Generic;
using System;

public class InteractableObject : MonoBehaviour, IInteractable
{
	#region Properties
	public ObjectType Type => _type;
	public List<InteractionOption> Options => _options;

	#endregion

	#region Fields
	[Header("Interaction Settings")]
	[SerializeField] private ObjectType _type;
	[SerializeField] private List<InteractionOption> _options = new();
	#endregion

}