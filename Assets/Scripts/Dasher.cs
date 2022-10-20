using UnityEngine;
using Mirror;

public class Dasher : MonoBehaviour
{
	[SerializeField] private PlayerMovement _playerMovement;
	[SerializeField] private PlayerMovementInput _playerMovementInput;
	[SerializeField] private CharacterController _characterController;

	[SerializeField] private float _dashDistance = 15.0f;

	private NetworkIdentity _networkIdentity;

	private void Update()
	{
		if (!_networkIdentity.isLocalPlayer) return;

		var dashInput = Input.GetKeyDown(KeyCode.Mouse0);
		if (!dashInput) return;

		var origin = _characterController.transform.position;
		var radius = _characterController.radius;
		var height = _characterController.height;

		var point1 = origin + radius * Vector3.up;
		var point2 = origin + height * Vector3.up - radius * Vector3.down;

		var direction = _playerMovementInput.InputDirection;

		var hit = Physics.CapsuleCast(point1, point2, radius,
			direction, out var hitInfo, _dashDistance);

		var distance = hit ? hitInfo.distance : _dashDistance;

		var newPosition = origin + direction * distance;

		_playerMovement.SetPosition(newPosition);
	}

	public void SetNetworkIdentity(NetworkIdentity networkIdentity)
	{
		_networkIdentity = networkIdentity;
	}
}
