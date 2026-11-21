using UnityEngine;
using System;

public class InteractionSystem : MonoBehaviour
{
    #region Properties
    public event Action<string> OnShowInteraction;
    public event Action OnHideInteraction;
    #endregion

    #region Fields
    [SerializeField] float _widthDetector = 2f;
    [SerializeField] float _distanceDetector = 3f;
    [SerializeField] float _originHeight = 1f;
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
                Debug.Log($"Collected {recolectable.Count} {recolectable.Type}, interaction system");
                recolectable.Interact();
            }
            else
            {
                _currentInteraction.Interact();
            }
        }
    }

    void FixedUpdate()
    {
        IInteractable closest = FindClosestInteractable();

        if (closest != null)
        {
            InteractableInfo info = closest.GetInfo();
            OnShowInteraction?.Invoke("E - To " + info.Action + " " + info.Type);
            Debug.Log("E - To " + info.Action + " " + info.Type);
            _interactebleDetected = true;
            _currentInteraction = closest;
            return;
        }

        if (_interactebleDetected)
        {
            _interactebleDetected = false;
            _currentInteraction = null;
            OnHideInteraction?.Invoke();
        }
    }

    private void OnDisable()
    {
        _interactebleDetected = false;
        _currentInteraction = null;
    }
    #endregion

    #region Public Methods
    public void ShowCustomMessage(string message)
    {
        OnShowInteraction?.Invoke(message);
    }

    public void HideMessage()
    {
        OnHideInteraction?.Invoke();
    }
    #endregion

    #region Private Methods
    private Vector3 GetDetectionCenter()
    {
        return transform.position + Vector3.up * _originHeight + transform.forward * _distanceDetector;
    }

    private IInteractable FindClosestInteractable()
    {
        Collider[] hits = Physics.OverlapSphere(GetDetectionCenter(), _widthDetector);

        IInteractable closest = null;
        float closestDistance = float.MaxValue;

        foreach (Collider hit in hits)
        {
            IInteractable interactable = hit.GetComponentInParent<IInteractable>();
            if (interactable == null) continue;

            float distance = (hit.transform.position - transform.position).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }

        return closest;
    }
    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetDetectionCenter(), _widthDetector);
    }
}