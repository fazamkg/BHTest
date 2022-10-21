using UnityEngine;
using System;
using Mirror;

public class Health : NetworkBehaviour
{
	[SerializeField] private MeshRenderer _meshRenderer;

	[SerializeField] private Color _hitColor = Color.red;
	[SerializeField] private float _noHitsTime = 3.0f;
	[SerializeField] private int _startHitPoints = 3;

	private Timer _timer;

	private int _hitPoints;
	private bool _canTakeHits = true;
	private Color _defaultColor;

	private void Awake()
	{
		_timer = GetComponent<Timer>();
		_timer.OnTimerReachedEnd += SetStateToCanTakeHits;
	}

	private void Start()
	{
		_defaultColor = _meshRenderer.material.color;
		_hitPoints = _startHitPoints;
	}

	[Server]
	private void SetStateToCanTakeHits()
	{
		_canTakeHits = true;
		_meshRenderer.material.color = _defaultColor;
		RpcUpdateState(_hitPoints, _canTakeHits, _defaultColor);
	}

	[ClientRpc]
	private void RpcUpdateState(int hitPoints, bool canTakeHits, Color color)
	{
		_hitPoints = hitPoints;
		_canTakeHits = canTakeHits;
		_meshRenderer.material.color = color;
	}

	[Server]
	public void TakeHit()
	{
		if (!_canTakeHits) return;
		if (_hitPoints <= 0) return;

		_hitPoints--;
		_canTakeHits = false;
		_meshRenderer.material.color = _hitColor;

		_timer.StartTimer(_noHitsTime);

		RpcUpdateState(_hitPoints, _canTakeHits, _hitColor);
	}
}
