using UnityEngine;
using System;
using StarterAssets;

public class GameModeController : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[Header("Game Modes")]
	[SerializeField] private GameObject _player;
	[SerializeField] private GameObject _raft;

	private GameObject _playerCamera;
	private GameObject _raftCamera;
	private ThirdPersonController _playerController;
	private RaftController _raftController;

	#endregion

	#region Unity Callbacks
	void Awake()
	{
		_playerCamera = Camera.main.gameObject;
		_raftCamera = _raft.GetComponentInChildren<Camera>().gameObject;

		_playerController = _player.GetComponent<ThirdPersonController>();
		_raftController = _raft.GetComponent<RaftController>();

	}

	void Start()
	{
		_playerCamera.SetActive(true);
		_raftCamera.SetActive(false);

		_playerController.enabled = true;
		_raftController.enabled = false;

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			SwitchGameMode();

		}


	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void SwitchGameMode()
	{
		_playerCamera.SetActive(!_playerCamera.activeSelf);
		_raftCamera.SetActive(!_raftCamera.activeSelf);

		_playerController.enabled = !_playerController.enabled;
		_raftController.enabled = !_raftController.enabled;
	}

	#endregion

}