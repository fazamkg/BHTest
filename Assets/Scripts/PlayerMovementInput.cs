using UnityEngine;
using Mirror;

public class PlayerMovementInput : MonoBehaviour
{
	[SerializeField] private CameraController _cameraController;

	[SerializeField] private string _horizontalInputName = "Horizontal";
	[SerializeField] private string _verticalInputName = "Vertical";

	private NetworkIdentity _networkIdentity;

	public Vector3 InputDirection { get; private set; }

	private void Update()
	{
		if (!_networkIdentity.isLocalPlayer) return;

		var horizontalAxis = Input.GetAxisRaw(_horizontalInputName);
		var verticalAxis = Input.GetAxisRaw(_verticalInputName);

		var globalInputDirection = new Vector3(horizontalAxis, 0.0f, verticalAxis);

		InputDirection = (_cameraController.YawRotation * globalInputDirection).normalized;
	}

	public void ConnectCamera(CameraController cameraController)
	{
		_cameraController = cameraController;
	}

	public void SetNetworkIdentity(NetworkIdentity networkIdentity)
	{
		_networkIdentity = networkIdentity;
	}
}
