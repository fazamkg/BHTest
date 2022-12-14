using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _acceleration = 40.0f;
	[SerializeField] private float _friction = 4.0f;
	[SerializeField] private float _gravity = 14.0f;

	private PlayerMovementInput _playerMovementInput;
	private CharacterController _characterController;
	private Interpolatable _interpolatable;

	private float _verticalVelocity;
	private Vector3 _horizontalVelocity;

	private void Awake()
	{
		_playerMovementInput = GetComponent<PlayerMovementInput>();
		_characterController = GetComponent<CharacterController>();
		_interpolatable = GetComponent<Interpolatable>();
	}

	private void Start()
	{
		_characterController.enableOverlapRecovery = true;
	}

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

	public void SetPosition(Vector3 position, bool skipInterpolation = false)
	{
		_characterController.enabled = false;
		if (skipInterpolation) _interpolatable.LastPosition = position;
		transform.position = position;
		_characterController.enabled = true;
	}
}
