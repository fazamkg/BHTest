using UnityEngine;

public class CameraController : MonoBehaviour
{
	private const float MaxAngle = 360.0f;

	[SerializeField] private string _mouseXInputName = "Mouse X";
	[SerializeField] private string _mouseYInputName = "Mouse Y";

	[SerializeField] private float _minPitch = -180.0f;
	[SerializeField] private float _maxPitch = 180.0f;

	[SerializeField] private float _sensitivity = 6.0f;

	[SerializeField] private bool _invertX;
	[SerializeField] private bool _invertY;

	private float _pitch;
	private float _yaw;

	private float Pitch
	{
		get => _pitch;
		set => _pitch = Mathf.Clamp(value, _minPitch, _maxPitch);
	}

	private float Yaw
	{
		get => _yaw;
		set => _yaw = Mathf.Repeat(value, MaxAngle);
	}

	public Quaternion YawRotation { get => Quaternion.Euler(0.0f, Yaw, 0.0f); }

	private void Update()
	{
		var mouseX = Input.GetAxisRaw(_mouseXInputName);
		var mouseY = Input.GetAxisRaw(_mouseYInputName);

		var deltaY = mouseY * _sensitivity;
		if (_invertY) deltaY *= -1.0f;

		var deltaX = mouseX * _sensitivity;
		if (_invertX) deltaX *= -1.0f;

		Pitch += deltaY;
		Yaw += deltaX;

		transform.rotation = Quaternion.Euler(Pitch, Yaw, 0.0f);
	}
}
