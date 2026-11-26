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
	private CharacterController _playerCharacterController;
	private Animator _playerAnimator;

	private RaftController _raftController;
	[SerializeField] private GameObject _raftDocked;
	[SerializeField] private GameObject _raftSailing;

	private bool _isPlayerInRaft = false;

	#endregion

	#region Unity Callbacks
	void Awake()
	{
		_playerCamera = Camera.main.gameObject;
		_raftCamera = _raft.GetComponentInChildren<Camera>().gameObject;

		_playerController = _player.GetComponent<ThirdPersonController>();
		_playerCharacterController = _player.GetComponent<CharacterController>();
		_playerAnimator = _player.GetComponent<Animator>();

		_raftController = _raft.GetComponent<RaftController>();
	}

	void Start()
	{
		_playerCamera.SetActive(true);
		_raftCamera.SetActive(false);

		_playerController.enabled = true;
		_playerCharacterController.enabled = true;

		_raftController.enabled = false;
		_raftDocked.SetActive(true);
		_raftSailing.SetActive(false);


	}

	#endregion

	#region Public Methods
	public void SwitchGameMode()
	{
		_playerCamera.SetActive(!_playerCamera.activeSelf);
		_raftCamera.SetActive(!_raftCamera.activeSelf);

		_playerController.enabled = !_playerController.enabled;
		_playerCharacterController.enabled = !_playerCharacterController.enabled;

		_raftController.enabled = !_raftController.enabled;
		_raftDocked.SetActive(!_raftDocked.activeSelf);
		_raftSailing.SetActive(!_raftSailing.activeSelf);

		_isPlayerInRaft = !_isPlayerInRaft;

		if (_isPlayerInRaft)
		{
			_player.transform.SetParent(_raft.transform);
			_player.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -1), Quaternion.identity);

			_playerAnimator.SetFloat("Speed", 0f);
			_playerAnimator.SetBool("Sitting", true);
		}
		else
		{
			_player.transform.SetParent(null);
			_playerAnimator.SetBool("Sitting", false);
		}

	}
	#endregion

	#region Private Methods
	#endregion

}