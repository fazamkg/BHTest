using Mirror;

public class Room : NetworkManager
{
	public override void OnServerConnect(NetworkConnectionToClient conn)
	{
		base.OnServerConnect(conn);
		print("someone connected");
	}
}
