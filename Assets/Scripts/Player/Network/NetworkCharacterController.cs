using UnityEngine;
using Mirror;

public class NetworkCharacterController : NetworkBehaviour
{
	[SerializeField] private CharacterController _characterController;

	private void FixedUpdate()
	{
		if (!hasAuthority) return;
		CmdUpdatePosition(_characterController.transform.position);
	}

	private void UpdatePosition(Vector3 position)
	{
		_characterController.enabled = false;
		_characterController.transform.position = position;
		_characterController.enabled = true;
	}

	[Command]
	private void CmdUpdatePosition(Vector3 position)
	{
		UpdatePosition(position);
		RpcUpdatePosition(position);
	}

	[ClientRpc]
	private void RpcUpdatePosition(Vector3 position)
	{
		if (hasAuthority) return;
		UpdatePosition(position);
	}
}
