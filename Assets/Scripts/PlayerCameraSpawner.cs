using UnityEngine;
using Mirror;

public class PlayerCameraSpawner : MonoBehaviour
{
	[SerializeField] private GameObject _camera;

	[SerializeField] private NetworkIdentity _networkIdentity;

	[SerializeField] private PlayerMovementInput _movementInput;

	private void Start()
	{
		if (!_networkIdentity.isLocalPlayer) return;

		var camera = Instantiate(_camera, transform);

		_movementInput.ConnectCamera(camera.GetComponent<CameraController>());
	}
}
