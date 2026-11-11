using UnityEngine;
using DG.Tweening;

public class RaftController : MonoBehaviour
{
	#region Properties

	#endregion

	#region Fields

	[Header("Basic Settings")]
	[SerializeField] private float raftSpeed = 8f;
	[SerializeField] private float rotationSpeed = 30f;
	[SerializeField] private LayerMask islandLayer;

	[Header("Dash Settings")]
	[SerializeField] private float dashSpeedMultiplier = 2f;
	[SerializeField] private float dashDuration = 1.5f;
	[SerializeField] private float extraFOV = 10f;

	private float currentSpeed;
	private float step;
	private bool isDashing = false;
	private readonly float raftRadius = 3f;
	private Camera raftCamera;

	#endregion

	#region Unity Callbacks
	void Awake()
	{
		raftCamera = GetComponentInChildren<Camera>();
		currentSpeed = raftSpeed;
	}

	void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
	}

	void Update()
	{
		float turn = (Input.mousePosition.x - Screen.width / 2f) / (Screen.width / 2f);
		turn = Mathf.Clamp(turn, -1f, 1f);
		if (Mathf.Abs(turn) < 0.1f) turn = 0f;

		transform.Rotate(0f, turn * rotationSpeed * Time.deltaTime, 0f);

		step = currentSpeed * Time.deltaTime;

		if (!IsIslandDetected())
		{
			transform.Translate(step * Vector3.forward);
		}

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			Dash();
		}
	}

	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void Dash()
	{
		if (!isDashing)
		{
			isDashing = true;
			currentSpeed = raftSpeed * dashSpeedMultiplier;
			raftCamera.DOFieldOfView(raftCamera.fieldOfView + extraFOV, dashDuration);
			Invoke(nameof(StopDash), dashDuration);
		}
	}

	private void StopDash()
	{
		isDashing = false;
		currentSpeed = raftSpeed;
		raftCamera.DOFieldOfView(raftCamera.fieldOfView - extraFOV, dashDuration).SetEase(Ease.InOutSine);
	}
	private bool IsIslandDetected()
	{
		return Physics.SphereCast(transform.position + Vector3.up * 0.3f, raftRadius, transform.forward, out _, step + 0.3f, islandLayer);
	}
	#endregion

}