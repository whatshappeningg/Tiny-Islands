using UnityEngine;
using System;

public class InteractionSystem : MonoBehaviour
{
    #region Properties
    public event Action<string> OnShowInteraction;
    public event Action OnHideInteraction;
    #endregion

    #region Fields
    [SerializeField] float _widthDetector = 2f;     // radio de la esfera
    [SerializeField] float _distanceDetector = 3f;    // alcance desde el player
    [SerializeField] float _originHeight = 1f;        // altura del pecho del player
    [SerializeField] Inventory _inventory;
    private bool _interactebleDetected = false;
    private IInteractable _currentInteraction;
    #endregion

    #region Unity Callbacks		
    private void Update()
    {
        if (_interactebleDetected && Input.GetKeyUp(KeyCode.E))
        {
            if (_currentInteraction is Recolectable recolectable)
            {
                _inventory.AddRecolectable(recolectable.Type, recolectable.Count);
                recolectable.Interact();
            }
            else
            {
                Debug.Log("Interacting with: " + _currentInteraction.GetInfo().Type);
                _currentInteraction.Interact();
            }
        }
    }
    void FixedUpdate()
    {
        Vector3 origin = transform.position + Vector3.up * _originHeight;

        if (Physics.SphereCast(origin, _widthDetector, transform.forward, out RaycastHit hit, _distanceDetector))
        {
            IInteractable interaction = hit.collider.GetComponent<IInteractable>();
            if (interaction != null)
            {
                InteractableInfo info = interaction.GetInfo();
                OnShowInteraction?.Invoke("E - To " + info.Action + " " + info.Type);
                Debug.Log("E - To " + info.Action + " " + info.Type);
                _interactebleDetected = true;
                _currentInteraction = interaction;
                return;
            }
        }

        _interactebleDetected = false;
        OnHideInteraction?.Invoke();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion

    void OnDrawGizmos()
    {
        Vector3 origin = transform.position + Vector3.up * _originHeight;
        Vector3 end = origin + transform.forward * _distanceDetector;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(origin, _widthDetector);
        Gizmos.DrawWireSphere(end, _widthDetector);
        Gizmos.DrawLine(origin, end);
    }
}