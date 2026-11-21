using UnityEngine;
using System;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
	#region Properties

	public Dictionary<RecolectableType, int> InventoryStack = new Dictionary<RecolectableType, int>();
	public Action OnInventoryUpdated;
	#endregion

	#region Fields
	#endregion

	#region Unity Callbacks
	#endregion

	#region Public Methods
	public void AddRecolectable(RecolectableType type, int count)
	{
		if (InventoryStack.ContainsKey(type))
			InventoryStack[type] += count;
		else
			InventoryStack.Add(type, count);
		OnInventoryUpdated?.Invoke();
		Debug.Log($"Collected {count} {type}, inventory system");
		Debug.Log($"Current {type} count: {InventoryStack[type]}");


	}
	public bool UseResource(RecolectableType type, int count = 1)
	{
		if (HasResource(type, count))
		{
			InventoryStack[type] -= count;
			OnInventoryUpdated?.Invoke();
			return true;
		}

		return false;
	}

	public bool HasResource(RecolectableType type, int count = 1)
	{
		if (InventoryStack.ContainsKey(type) && InventoryStack[type] >= count)
			return true;

		return false;
	}
	#endregion

	#region Private Methods
	#endregion
}
