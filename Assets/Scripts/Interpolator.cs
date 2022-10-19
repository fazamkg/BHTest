using UnityEngine;

public class Interpolator : MonoBehaviour
{
	[SerializeField] private Interpolatable _target;

	private void Update()
	{
		var weight = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;

		InterpolateRotation(weight);
		InterpolatePosition(weight);
	}

	private void InterpolateRotation(float weight)
	{
		var leftValue = _target.LastRotation;
		var rightValue = _target.transform.rotation;

		transform.rotation = Quaternion.Lerp(leftValue, rightValue, weight);
	}

	private void InterpolatePosition(float weight)
	{
		var leftValue = _target.LastPosition;
		var rightValue = _target.transform.position;

		transform.position = Vector3.Lerp(leftValue, rightValue, weight);
	}
}
