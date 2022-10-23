using UnityEngine;
using Mirror;

public class NonLocalPlayerGameObjectDisabler : MonoBehaviour
{
	private void Start()
	{
		if (GetComponentInParent<NetworkIdentity>().isLocalPlayer) return;
		gameObject.SetActive(false);
	}
}
