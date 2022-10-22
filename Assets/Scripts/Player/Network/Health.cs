using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour
{
	[SerializeField] private Timer _timer;
	[SerializeField] private MeshRenderer _meshRenderer;

	[SerializeField] private Color _hitColor = Color.red;
	[SerializeField] private float _noHitsTime = 3.0f;

	private bool _canTakeHits = true;
	private Color _defaultColor;

	private void Awake()
	{
		_timer.OnTimerReachedEnd += SetStateToCanTakeHits;
	}

	private void Start()
	{
		_defaultColor = _meshRenderer.material.color;
	}

	[Server]
	private void SetStateToCanTakeHits()
	{
		_canTakeHits = true;
		_meshRenderer.material.color = _defaultColor;
		RpcUpdateState(_canTakeHits, _defaultColor);
	}

	[ClientRpc]
	private void RpcUpdateState(bool canTakeHits, Color color)
	{
		_canTakeHits = canTakeHits;
		_meshRenderer.material.color = color;
	}

	[Server]
	public void TakeHit(NetworkIdentity source)
	{
		if (!_canTakeHits) return;

		var scoreTaker = source.GetComponent<ScoreTaker>();
		if (scoreTaker) scoreTaker.AddScore(1);

		_canTakeHits = false;
		_meshRenderer.material.color = _hitColor;

		_timer.StartTimer(_noHitsTime);

		RpcUpdateState(_canTakeHits, _hitColor);
	}
}
