using UnityEngine;
using System;

public class RaftController : MonoBehaviour
{
	#region Properties

	#endregion

	#region Fields
	[SerializeField] private float raftSpeed = 8f;
	[SerializeField] private float rotationSpeed = 30f;
	private Transform cameraTransform;

	#endregion

	#region Unity Callbacks
	void Awake()
	{
		cameraTransform = Camera.main.transform;
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
		transform.Translate(Vector3.forward * raftSpeed * Time.deltaTime);
	}

	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion

}