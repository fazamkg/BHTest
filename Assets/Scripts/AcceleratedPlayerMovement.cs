using UnityEngine;

public class AcceleratedPlayerMovement : MonoBehaviour
{
	[SerializeField] private PlayerMovementInput _playerMovementInput;

	[SerializeField] private CharacterController _characterController;

	[SerializeField] private float _acceleration = 40.0f;
	[SerializeField] private float _friction = 4.0f;
	[SerializeField] private float _gravity = 14.0f;

	private Vector3 _horizontalVelocity;
	private float _verticalVelocity;

	private void FixedUpdate()
	{
		var delta = _acceleration * Time.fixedDeltaTime * _playerMovementInput.InputDirection;
		_horizontalVelocity += delta;

		var frictionDelta = _friction * Time.fixedDeltaTime * _horizontalVelocity;
		if (_horizontalVelocity.sqrMagnitude >= frictionDelta.sqrMagnitude)
		{
			_horizontalVelocity -= frictionDelta;
		}
		else
		{
			_horizontalVelocity = Vector3.zero;
		}

		_verticalVelocity += _gravity * Time.fixedDeltaTime * -1.0f;

		_characterController.Move(_horizontalVelocity * Time.fixedDeltaTime);
		_horizontalVelocity = _characterController.velocity;
		_horizontalVelocity.y = 0.0f;

		_characterController.Move(_verticalVelocity * Time.fixedDeltaTime * Vector3.up);
		_verticalVelocity = _characterController.velocity.y;
	}
}
