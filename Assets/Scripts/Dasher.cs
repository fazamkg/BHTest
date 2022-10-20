using UnityEngine;
using Mirror;

public class Dasher : MonoBehaviour
{
	[SerializeField] private PlayerMovement _playerMovement;
	[SerializeField] private PlayerMovementInput _playerMovementInput;
	[SerializeField] private CharacterController _characterController;

	[SerializeField] private float _dashDistance = 15.0f;
	[SerializeField] private float _castMargin = 0.25f;

	private NetworkIdentity _networkIdentity;

	private Health _currentEnemyHealth;

	private void Update()
	{
		if (!_networkIdentity.isLocalPlayer) return;

		var dashInput = Input.GetKeyDown(KeyCode.Mouse0);
		if (!dashInput) return;

		var direction = _playerMovementInput.InputDirection;
		if (direction == Vector3.zero) return;

		var origin = _characterController.transform.position;
		var radius = _characterController.radius;
		var height = _characterController.height;

		var point1 = origin + radius * Vector3.up;
		var point2 = origin + height * Vector3.up - radius * Vector3.down;

		var hit = Physics.CapsuleCast(point1, point2, radius,
			direction, out var hitInfo, _dashDistance);

		if (hitInfo.collider != null)
		{
			var hasEnemyHealth = hitInfo.collider.TryGetComponent(out _currentEnemyHealth);
			if (hasEnemyHealth) _currentEnemyHealth.TakeHit();
		}

		var distance = hit ? Mathf.Max(hitInfo.distance - _castMargin, 0.0f) : _dashDistance;

		var newPosition = origin + direction * distance;

		_playerMovement.SetPosition(newPosition, true);
	}

	public void SetNetworkIdentity(NetworkIdentity networkIdentity)
	{
		_networkIdentity = networkIdentity;
	}
}
