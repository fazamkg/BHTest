using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ScoreTaker : NetworkBehaviour
{
	[SerializeField] private Text _scoreText; 

	private int _score;

	[Client]
	private void UpdateUI()
	{
		if (!isLocalPlayer) return;
		_scoreText.text = _score.ToString();
	}

	[ClientRpc]
	private void RpcUpdateScoreOnClients(int score)
	{
		_score = score;
		UpdateUI();
	}

	[Server]
	public void AddScore(int score)
	{
		_score += score;
		RpcUpdateScoreOnClients(_score);
	}
}
