using UnityEngine;
using System;

public enum RecolectableType
{
    Wood,
    Cannonball,
}
public class Recolectable : MonoBehaviour, IInteractable
{
    #region Properties
    [field: SerializeField] public int Count { get; set; }
    [field: SerializeField] public RecolectableType Type { get; set; }

    #endregion

    #region Fields
    private InteractableInfo _interactableInfo;
    #endregion

    #region Unity Callbacks

    #endregion

    #region Public Methods
    public InteractableInfo GetInfo()
    {
        InteractableInfo info = new()
        {
            Action = "collect",
            Type = Type.ToString().ToLower()
        };
        return info;
    }
    public void Interact()
    {
        Destroy(gameObject);
    }
    #endregion

    #region Private Methods
    #endregion
}