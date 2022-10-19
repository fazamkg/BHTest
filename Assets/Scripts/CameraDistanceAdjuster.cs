using UnityEngine;

public class CameraDistanceAdjuster : MonoBehaviour
{
	[SerializeField] private Transform _cameraTransform;

	[SerializeField] private float _maxDistance = 7.5f;
	[SerializeField] private float _margin = 1.5f;
	[SerializeField] private float _minDistance = 2.0f;

	private void Update()
	{
		var ray = new Ray(transform.position, -transform.forward);
		var somethingGotHit = Physics.Raycast(ray, out var hitInfo, _maxDistance);

		var position = _cameraTransform.localPosition;

		position.z = somethingGotHit ? -hitInfo.distance : -_maxDistance;
		position.z += _margin;
		position.z = Mathf.Min(position.z, -_minDistance);

		_cameraTransform.localPosition = position;
	}
}
