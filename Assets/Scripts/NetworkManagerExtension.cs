using Mirror;
using System;

public class NetworkManagerExtension : NetworkManager
{
	public event Action OnClientConnected;
	public event Action OnClientDisconnected;

	public override void OnClientConnect()
	{
		base.OnClientConnect();
		OnClientConnected?.Invoke();
	}

	public override void OnClientDisconnect()
	{
		base.OnClientDisconnect();
		OnClientDisconnected?.Invoke();
	}
}
