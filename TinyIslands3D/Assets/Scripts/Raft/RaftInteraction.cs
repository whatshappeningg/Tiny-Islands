using UnityEngine;
using System;

public class RaftInteraction : InteractableObject
{
    #region Properties
    public event Action OnChangeGameMode;

    #endregion

    #region Fields
    [SerializeField] private InteractionSystem _interactionSystem;
    private bool _isRaftDetected = false;
    #endregion

    #region Unity Callbacks
    void Update()
    {

    }
    #endregion

    #region Public Methods
    public void Interact()
    {
        OnChangeGameMode?.Invoke();
    }

    #endregion

    #region Private Methods

    #endregion

}