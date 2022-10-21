using UnityEngine;

public class Interpolator : MonoBehaviour
{
	[SerializeField] private Interpolatable _target;
	[SerializeField] private bool _off;

	private void Update()
	{
		if (_off)
		{
			transform.position = _target.transform.position;
			return;
		}

		var weight = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;

		var leftValue = _target.LastPosition;
		var rightValue = _target.transform.position;

		transform.position = Vector3.Lerp(leftValue, rightValue, weight);
	}

	public void SetTarget(Interpolatable target) => _target = target;
}
