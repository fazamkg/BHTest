using UnityEngine;

public class CursorLocker : MonoBehaviour
{
	[SerializeField] private NetworkManagerExtension _networkManager;

	private bool _isLocked;

	private void Awake()
	{
		_networkManager.OnClientConnected += Lock;
	}

	private void Start()
	{
		Unlock();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) ToogleLock();
	}

	private void ToogleLock()
	{
		if (_isLocked)
		{
			Unlock();
		}
		else
		{
			Lock();
		}
	}

	private void Lock()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		_isLocked = true;
	}

	private void Unlock()
	{
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
		_isLocked = false;
	}
}
