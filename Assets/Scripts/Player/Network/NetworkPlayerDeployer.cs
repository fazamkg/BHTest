using UnityEngine;
using Mirror;

public class NetworkPlayerDeployer : NetworkBehaviour
{
	[SerializeField] private GameObject _gameObjectToDeploy;
	[SerializeField] private GameObject _gameObjectToDisable;

	private void Start()
	{
		_gameObjectToDeploy.transform.parent = null;

		if (hasAuthority) return;
		_gameObjectToDisable.SetActive(false);
	}
}
