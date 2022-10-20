using UnityEngine;

public class MenuCamera : MonoBehaviour
{
	[SerializeField] private NetworkManagerExtension _networkManager;

	private void Awake()
	{
		_networkManager.OnClientConnected += Disable;
		_networkManager.OnClientDisconnected += Enable;
	}

	private void Disable() => gameObject.SetActive(false);

	private void Enable() => gameObject.SetActive(true);
}
