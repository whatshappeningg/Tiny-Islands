using UnityEngine;
using System;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
	#region Properties

	public Dictionary<ObjectType, int> InventoryStack = new Dictionary<ObjectType, int>();
	public Action OnInventoryUpdated;
	#endregion

	#region Fields
	#endregion

	#region Unity Callbacks
	#endregion

	#region Public Methods
	public void AddRecolectable(ObjectType type, int count)
	{
		if (InventoryStack.ContainsKey(type))
			InventoryStack[type] += count;
		else
			InventoryStack.Add(type, count);
		OnInventoryUpdated?.Invoke();
		Debug.Log($"Current {type} count: {InventoryStack[type]}");


	}
	public bool UseResource(ObjectType type, int count = 1)
	{
		if (HasResource(type, count))
		{
			InventoryStack[type] -= count;
			OnInventoryUpdated?.Invoke();
			return true;
		}

		return false;
	}

	public bool HasResource(ObjectType type, int count = 1)
	{
		if (InventoryStack.ContainsKey(type) && InventoryStack[type] >= count)
			return true;

		return false;
	}
	#endregion

	#region Private Methods
	#endregion
}
