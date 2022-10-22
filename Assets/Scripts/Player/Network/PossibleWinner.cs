using UnityEngine;
using System;
using Mirror;
using UnityEngine.UI;

public class PossibleWinner : NetworkBehaviour
{
	[SerializeField] private int _scoreToWin = 3;

	[SerializeField] private Text _winText;

	private ScoreTaker _scoreTaker;

	private static bool _somebodyWon;

	public event Action OnWin;
	public static event Action<NetworkIdentity> OnPlayerWon;

	private void Awake()
	{
		_scoreTaker = GetComponent<ScoreTaker>();

		_scoreTaker.OnScoreUpdated += CheckIfPlayerWon;
	}

	public override void OnStartAuthority()
	{
		base.OnStartAuthority();

		OnPlayerWon += ShowWinnerUI;
	}

	private void ShowWinnerUI(NetworkIdentity winner)
	{
		_winText.gameObject.SetActive(true);
		_winText.text = $"[{winner.netId}] wins the game!";
	}

	[ClientRpc]
	private void RpcNotifyClientsAboutWinner(NetworkIdentity winner)
	{
		_somebodyWon = true;
		OnPlayerWon?.Invoke(winner);
	}

	[Server]
	private void CheckIfPlayerWon(int score)
	{
		if (!_somebodyWon && score >= _scoreToWin)
		{
			_somebodyWon = true;
			OnWin?.Invoke();
			OnPlayerWon?.Invoke(netIdentity);
			RpcNotifyClientsAboutWinner(netIdentity);
		}
	}
}