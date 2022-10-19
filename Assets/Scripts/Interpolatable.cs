using UnityEngine;

public class Interpolatable : MonoBehaviour
{
	public Quaternion LastRotation { get; set; }
	public Vector3 LastPosition { get; set; }

	private void FixedUpdate()
	{
		LastPosition = transform.position;
		LastRotation = transform.rotation;
	}
}
