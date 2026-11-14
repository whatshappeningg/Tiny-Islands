using UnityEngine;
using DG.Tweening;

public class CannonController : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[Header("Target Settings")]
	[SerializeField] private Transform _impactPoint;
	[Header("Cannon Settings")]
	[SerializeField] private Transform _cannonball;
	[SerializeField] private ParticleSystem _smokeParticles;

	private readonly float _arcHeight = 5f;
	private readonly float _flightTime = 1.5f;
	private Vector3 ballStartLocalPos;
	private bool _canFire = true;

	#endregion

	#region Unity Callbacks
	void Awake()
	{
		ballStartLocalPos = _cannonball.localPosition;
	}
	void Start()
	{
		_cannonball.gameObject.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (_canFire) Fire();
			else Debug.Log("Already been fired!");
		}
	}
	#endregion

	#region Public Methods
	public void Fire()
	{
		_cannonball.gameObject.SetActive(true);

		_smokeParticles.Play();

		_cannonball.DOJump(_impactPoint.position, _arcHeight, 1, _flightTime)
			.SetEase(Ease.Linear)
			.SetLink(_cannonball.gameObject)
			.OnComplete(() =>
			{
				_cannonball.gameObject.SetActive(false);
				_cannonball.localPosition = ballStartLocalPos;
				//_canFire = false;
			});
	}
	#endregion

	#region Private Methods
	#endregion
}