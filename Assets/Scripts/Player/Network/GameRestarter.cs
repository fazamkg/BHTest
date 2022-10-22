using UnityEngine;
using Mirror;

public class GameRestarter : NetworkBehaviour
{
	[Scene] [SerializeField] private string _mainScene;

	[SerializeField] private Timer _timer;

	[SerializeField] private float _resetTimeAfterWin = 5.0f;

	private PossibleWinner _possibleWinner;

	private void Awake()
	{
		_possibleWinner = GetComponent<PossibleWinner>();

		_timer.OnTimerReachedEnd += RestartGame;
		_possibleWinner.OnWin += StartTimer;
	}

	private void StartTimer() => _timer.StartTimer(_resetTimeAfterWin);

	[Server]
	private void RestartGame()
	{
		NetworkManager.singleton.ServerChangeScene(_mainScene);
	}
}
