using UnityEngine;
using System;

public class Health : MonoBehaviour
{
	[SerializeField] private MeshRenderer _meshRenderer;

	[SerializeField] private Timer _timer;

	[SerializeField] private Color _invulnerabilityColor = Color.red;
	[SerializeField] private float _invulnerabilityTime = 3.0f;
	[SerializeField] private int _startHitPoints = 3;

	private bool _isVulnerable = true;
	private Color _defaultColor;
	private int _hitPoints;

	public event Action<int> OnHitPointsUpdate;

	private void Awake()
	{
		_timer.OnTimerReachedEnd += SetVulnerable;
	}

	private void Start()
	{
		_defaultColor = _meshRenderer.material.color;
		_hitPoints = _startHitPoints;

		SetVulnerable();
	}

	private void SetInvulnerable() => SetVulnerabilityState(false);

	private void SetVulnerable() => SetVulnerabilityState(true);

	private void SetVulnerabilityState(bool state)
	{
		_isVulnerable = state;

		_meshRenderer.material.color = _isVulnerable ? _defaultColor : _invulnerabilityColor;
	}

	public void TakeHit()
	{
		if (!_isVulnerable) return;
		if (_hitPoints <= 0) return;
		_hitPoints--;
		OnHitPointsUpdate?.Invoke(_hitPoints);
		_timer.StartTimer(_invulnerabilityTime);
		SetInvulnerable();
	}

	public void SetMeshRenderer(MeshRenderer meshRenderer) => _meshRenderer = meshRenderer;
}
