using UnityEngine;

public class CursorLocker : MonoBehaviour
{
	[SerializeField] private bool _canBeToggled;
	[SerializeField] private bool _startLocked;

	private void Start()
	{
		if (_startLocked)
		{
			Lock();
		}
		else
		{
			Unlock();
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) ToogleLock();
	}

	private void ToogleLock()
	{
		if (!_canBeToggled) return;
		if (Cursor.lockState == CursorLockMode.Locked)
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
	}

	private void Unlock()
	{
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
	}
}
