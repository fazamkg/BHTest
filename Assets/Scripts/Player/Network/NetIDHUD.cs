using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class NetIDHUD : MonoBehaviour
{
	private void Start()
	{
		var networkIdentity = GetComponentInParent<NetworkIdentity>();
		GetComponent<Text>().text = $"netId: [{networkIdentity.netId}]";
	}
}
