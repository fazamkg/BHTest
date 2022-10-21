using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;

public class ScoreTaker : NetworkBehaviour
{
	[SerializeField] private Text _scoreText;
	[SerializeField] private Text _winText;

	[SerializeField] private int _scoreToWin = 3;

	private int _score;

	private static bool _somebodyWon;

	private static event Action<string> OnPlayerWon;

	public override void OnStartAuthority()
	{
		base.OnStartAuthority();

		OnPlayerWon += ShowWinUI;
	}

	private void ShowWinUI(string message)
	{
		_winText.gameObject.SetActive(true);
		_winText.text = message;
	}

	[Client]
	private void UpdateScoreUI()
	{
		if (!isLocalPlayer) return;
		_scoreText.text = _score.ToString();
	}

	[ClientRpc]
	private void RpcUpdateScoreOnClients(int score)
	{
		_score = score;
		UpdateScoreUI();
	}

	[ClientRpc]
	private void RpcNotifyClientsAboutWinner(NetworkIdentity winner)
	{
		_somebodyWon = true;

		OnPlayerWon?.Invoke($"[{winner.netId}] wins the game!");
	}
	
	[Server]
	private void CheckIfPlayerWon()
	{
		if (!_somebodyWon && _score >= _scoreToWin)
		{
			_somebodyWon = true;
			RpcNotifyClientsAboutWinner(netIdentity);
		}
	}

	[Server]
	public void AddScore(int score)
	{
		_score += score;
		CheckIfPlayerWon();
		RpcUpdateScoreOnClients(_score);
	}
}
