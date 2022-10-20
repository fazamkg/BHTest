using UnityEngine;

public class PlayerMovementInput : MonoBehaviour
{
	[SerializeField] private CameraController _cameraController;

	[SerializeField] private string _horizontalInputName = "Horizontal";
	[SerializeField] private string _verticalInputName = "Vertical";

	public Vector3 InputDirection { get; private set; }

	private void Update()
	{
		var horizontalAxis = Input.GetAxisRaw(_horizontalInputName);
		var verticalAxis = Input.GetAxisRaw(_verticalInputName);

		var globalInputDirection = new Vector3(horizontalAxis, 0.0f, verticalAxis);
		InputDirection = (_cameraController.YawRotation * globalInputDirection).normalized;
	}
}
