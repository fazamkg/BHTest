using UnityEngine;
using Mirror;

public class PlayerInitializer : NetworkBehaviour
{
	[SerializeField] private GameObject _characterController;
	[SerializeField] private GameObject _visuals;
	[SerializeField] private GameObject _camera;

	[SerializeField] private Vector3 _cameraPosition;

	private PlayerMovement _playerMovement;
	private Interpolatable _interpolatable;
	private PlayerMovementInput _movementInput;

	private void Start()
	{
		var characterController = Instantiate(_characterController,
			transform.position, Quaternion.identity);
		_playerMovement = characterController.GetComponent<PlayerMovement>();
		_interpolatable = characterController.GetComponent<Interpolatable>();
		_movementInput = characterController.GetComponent<PlayerMovementInput>();
		_movementInput.SetNetworkIdentity(netIdentity);

		var visuals = Instantiate(_visuals, transform.position, transform.rotation);
		visuals.GetComponent<Interpolator>().SetTarget(_interpolatable);

		if (!isLocalPlayer) return;
		var cameraTransform = new GameObject("CameraTransform");
		cameraTransform.transform.parent = visuals.transform;
		cameraTransform.transform.localPosition = _cameraPosition;
		var camera = Instantiate(_camera, cameraTransform.transform);
		_movementInput.ConnectCamera(camera.GetComponent<CameraController>());
	}

	private void FixedUpdate()
	{
		CopyPosition();
		PastePosition();
	}

	private void CopyPosition()
	{
		if (!isLocalPlayer) return;

		transform.position = _playerMovement.transform.position;
	}

	private void PastePosition()
	{
		if (isLocalPlayer) return;

		_playerMovement.SetPosition(transform.position);
	}
}
