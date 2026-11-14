using UnityEngine;
using DG.Tweening;
using System;

public class DestructibleRock : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[Header("Rock Settings")]
	[SerializeField] private int _hitsNeeded = 1;
	[SerializeField] private Transform _impactPoint;

	[Header("Particle Systems")]
	[SerializeField] private ParticleSystem _rockParticles;
	[SerializeField] private ParticleSystem _destroyedParticles;
	[SerializeField] private ParticleSystem _dustParticles;
	private readonly float _sinkDepth = 15f;
	private readonly float _sinkDuration = 3f;
	private readonly float _shakeStrength = 1f;
	private int _currentHits = 0;
	#endregion

	#region Unity Callbacks
	void Awake()
	{
		if (_rockParticles == null) Debug.LogError("Rock Particles not assigned in the inspector!");
		if (_destroyedParticles == null) Debug.LogError("Destroyed Particles not assigned in the inspector!");
		if (_dustParticles == null) Debug.LogError("Dust Particles not assigned in the inspector!");
	}
	void Start()
	{
		_rockParticles.transform.position = _impactPoint.position;
		_destroyedParticles.transform.position = _impactPoint.position;
		_dustParticles.transform.position = _impactPoint.position;
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Cannonball"))
		{
			_currentHits++;
			_rockParticles.Play();

			if (_currentHits >= _hitsNeeded)
			{
				DestroyRock();
			}
		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void DestroyRock()
	{
		_destroyedParticles.Play();
		_dustParticles.Play();

		Vector3 startPos = transform.position;

		Sequence seq = DOTween.Sequence().SetLink(gameObject);
		seq.Append(transform.DOMoveY(startPos.y - _sinkDepth, _sinkDuration).SetEase(Ease.InQuad));
		seq.Join(transform.DOShakeRotation(_sinkDuration, new Vector3(_shakeStrength, 0f, _shakeStrength), 30, 90f, false));
		seq.OnComplete(() =>
		{
			Destroy(gameObject);
		});
	}

	#endregion

}