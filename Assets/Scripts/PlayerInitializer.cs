using UnityEngine;
using Mirror;

public class PlayerInitializer : NetworkBehaviour
{
	[SerializeField] private NetworkTransform _networkTransform;

	[SerializeField] private GameObject _characterController;
	[SerializeField] private GameObject _visuals;
	[SerializeField] private GameObject _camera;

	[SerializeField] private Vector3 _cameraPosition;

	private PlayerMovement _playerMovement;

	private void Start()
	{
		var characterController = Instantiate(_characterController,
			transform.position, Quaternion.identity);
		_playerMovement = characterController.GetComponent<PlayerMovement>();
		var interpolatable = characterController.GetComponent<Interpolatable>();
		var movementInput = characterController.GetComponent<PlayerMovementInput>();
		var dasher = characterController.GetComponent<Dasher>();
		var health = characterController.GetComponent<Health>();
		movementInput.SetNetworkIdentity(netIdentity);
		dasher.SetNetworkIdentity(netIdentity);
		_playerMovement.SetNetworkTransform(_networkTransform);

		var visuals = Instantiate(_visuals, transform.position, transform.rotation);
		visuals.GetComponent<Interpolator>().SetTarget(interpolatable);
		var meshRenderer = visuals.GetComponentInChildren<MeshRenderer>();
		health.SetMeshRenderer(meshRenderer);

		if (!isLocalPlayer) return;
		var cameraTransform = new GameObject("CameraTransform");
		cameraTransform.transform.parent = visuals.transform;
		cameraTransform.transform.localPosition = _cameraPosition;
		var camera = Instantiate(_camera, cameraTransform.transform);
		movementInput.ConnectCamera(camera.GetComponent<CameraController>());
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
