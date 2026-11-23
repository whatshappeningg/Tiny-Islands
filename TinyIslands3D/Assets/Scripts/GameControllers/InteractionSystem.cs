using UnityEngine;
using System;
using System.Text;

public class InteractionSystem : MonoBehaviour
{
    #region Properties
    public event Action<string> OnShowInteraction;
    public event Action OnHideInteraction;

    public event Action<IInteractable, InteractionOption> OnInteracted;

    public IInteractable Current => _currentInteraction;
    #endregion

    #region Fields
    [SerializeField] float _widthDetector = 2f;
    [SerializeField] float _distanceDetector = 3f;
    [SerializeField] float _originHeight = 1f;

    private const int MaxHits = 16;
    private readonly Collider[] _hits = new Collider[MaxHits];
    private readonly StringBuilder _builder = new();

    private IInteractable _currentInteraction;
    #endregion

    #region Unity Callbacks
    private void Update()
    {
        if (_currentInteraction == null) return;

        foreach (InteractionOption option in _currentInteraction.Options)
        {
            if (Input.GetKeyUp(option.Key))
            {
                OnInteracted?.Invoke(_currentInteraction, option);
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        IInteractable closest = FindClosestInteractable();

        if (ReferenceEquals(closest, _currentInteraction)) return;

        _currentInteraction = closest;

        if (_currentInteraction != null)
            //OnShowInteraction?.Invoke(BuildMessage(_currentInteraction));
            Debug.Log(BuildMessage(_currentInteraction));
        else
            OnHideInteraction?.Invoke();
    }

    private void OnDisable()
    {
        if (_currentInteraction != null)
        {
            _currentInteraction = null;
            OnHideInteraction?.Invoke();
        }
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

    // ??
    public void RefreshMessage()
    {
        if (_currentInteraction != null)
            OnShowInteraction?.Invoke(BuildMessage(_currentInteraction));
    }
    #endregion

    #region Private Methods
    private Vector3 GetDetectionCenter()
    {
        return transform.position + Vector3.up * _originHeight + transform.forward * _distanceDetector;
    }

    private IInteractable FindClosestInteractable()
    {
        int count = Physics.OverlapSphereNonAlloc(GetDetectionCenter(), _widthDetector, _hits);

        IInteractable closest = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            IInteractable interactable = _hits[i].GetComponentInParent<IInteractable>();
            if (interactable == null) continue;

            float distance = (_hits[i].transform.position - transform.position).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }

        return closest;
    }

    private string BuildMessage(IInteractable target)
    {
        _builder.Clear();

        foreach (InteractionOption option in target.Options)
        {
            if (_builder.Length > 0) _builder.Append('\n');

            _builder.Append(option.Key).Append(" - ").Append(option.Action);
        }

        return _builder.ToString();
    }
    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetDetectionCenter(), _widthDetector);
    }
}