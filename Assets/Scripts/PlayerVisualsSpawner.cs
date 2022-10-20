using UnityEngine;
using Mirror;

public class PlayerVisualsSpawner : MonoBehaviour
{
	[SerializeField] private GameObject _visuals;
	[SerializeField] private Interpolatable _interpolatable;

	[SerializeField] private NetworkIdentity _networkIdentity;
	[SerializeField] private GameObject _camera;
	[SerializeField] private Vector3 _cameraPosition;
	[SerializeField] private PlayerMovementInput _movementInput;

	private void Start()
	{
		var visuals = Instantiate(_visuals, transform.position, transform.rotation);
		visuals.GetComponent<Interpolator>().SetTarget(_interpolatable);

		if (!_networkIdentity.isLocalPlayer) return;
		var cameraTransform = new GameObject("CameraTransform");
		cameraTransform.transform.parent = visuals.transform;
		cameraTransform.transform.localPosition = _cameraPosition;
		var camera = Instantiate(_camera, cameraTransform.transform);
		_movementInput.ConnectCamera(camera.GetComponent<CameraController>());
	}
}
