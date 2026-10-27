using UnityEngine;
using DG.Tweening;

public class RaftController : MonoBehaviour
{
	#region Properties

	#endregion

	#region Fields
	[Header("Movement Settings")]
	[SerializeField] private float raftSpeed = 8f;
	[SerializeField] private float rotationSpeed = 30f;

	[Header("Dash Settings")]
	[SerializeField] private float dashSpeedMultiplier = 2f;
	[SerializeField] private float dashDuration = 1.5f;
	[SerializeField] private float extraFOV = 10f;

	private bool isDashing = false;
	private float currentSpeed;
	private float limitHeight;

	private Camera mainCamera;

	#endregion

	#region Unity Callbacks
	void Awake()
	{
		mainCamera = Camera.main;
		currentSpeed = raftSpeed;
		limitHeight = transform.position.y;
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
		transform.Translate(currentSpeed * Time.deltaTime * Vector3.forward);
		//transform.position = new Vector3(transform.position.x, limitHeight, transform.position.z);

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			Dash();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Terrain"))
			currentSpeed = raftSpeed * 0.5f;
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
			mainCamera.DOFieldOfView(mainCamera.fieldOfView + extraFOV, dashDuration);
			Invoke(nameof(StopDash), dashDuration);
		}
	}

	private void StopDash()
	{
		isDashing = false;
		currentSpeed = raftSpeed;
		mainCamera.DOFieldOfView(mainCamera.fieldOfView - extraFOV, dashDuration).SetEase(Ease.InOutSine);
	}
	#endregion

}