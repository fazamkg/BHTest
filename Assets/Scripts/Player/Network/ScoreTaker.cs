using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;

public class ScoreTaker : NetworkBehaviour
{
	[SerializeField] private Text _scoreText;

	private int _score;

	public event Action<int> OnScoreUpdated;

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

	[Server]
	public void AddScore(int score)
	{
		_score += score;
		OnScoreUpdated?.Invoke(_score);
		RpcUpdateScoreOnClients(_score);
	}
}
