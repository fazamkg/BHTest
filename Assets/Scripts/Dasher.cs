using UnityEngine;
using Mirror;

public class Dasher : MonoBehaviour
{
	[SerializeField] private PlayerMovement _playerMovement;
	[SerializeField] private PlayerMovementInput _playerMovementInput;

	[SerializeField] private float _dashDistance = 3.0f;
	[SerializeField] private float _dashTime = 0.05f;

	private NetworkIdentity _networkIdentity;

	private void Update()
	{
		if (!_networkIdentity.isLocalPlayer) return;

		var dashInput = Input.GetKeyDown(KeyCode.Mouse0);
		if (!dashInput) return;

		var dashSpeed = _dashDistance / _dashTime;

		_playerMovement.AddImpulse(dashSpeed * _playerMovementInput.InputDirection);
	}

	public void SetNetworkIdentity(NetworkIdentity networkIdentity)
	{
		_networkIdentity = networkIdentity;
	}
}
