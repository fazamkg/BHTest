using UnityEngine;
using Mirror;

public class NetworkPlayerChildDeployer : NetworkBehaviour
{
	[SerializeField] private GameObject _gameObjectToDisable;

	private void Start()
	{
		transform.GetChild(0).parent = null;

		if (hasAuthority) return;
		_gameObjectToDisable.SetActive(false);
	}
}
