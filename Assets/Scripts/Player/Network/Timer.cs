using UnityEngine;
using System;
using Mirror;

public class Timer : NetworkBehaviour
{
	private float _timeInterval;
	private float _timer;
	private bool _isDone = true;

	public event Action OnTimerReachedEnd;

	private void Update()
	{
		if (_isDone) return;
		_timer += Time.deltaTime;
		if (_timer < _timeInterval) return;

		OnTimerReachedEnd?.Invoke();
		_isDone = true;
		_timer = 0.0f;
	}

	public void StartTimer(float timeInterval)
	{
		_timeInterval = timeInterval;
		_isDone = false;
	}
}
