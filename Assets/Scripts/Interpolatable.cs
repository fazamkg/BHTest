using UnityEngine;

public class Interpolatable : MonoBehaviour
{
	public Vector3 LastPosition { get; set; }

	private void FixedUpdate()
	{
		LastPosition = transform.position;
	}
}
