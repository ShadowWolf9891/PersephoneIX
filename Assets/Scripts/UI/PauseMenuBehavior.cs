using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehavior : MonoBehaviour
{
	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject inventoryPanel;
	[SerializeField] GameObject PlayerHUD;

	[SerializeField] GameEvent toggleMovementEvent;
	[SerializeField] GameEvent togglePauseEvent;
	[SerializeField] GameEvent toggleInventory;

	public void ToggleMenu()	
	{
		if (pauseMenu.activeInHierarchy)
		{
			pauseMenu.SetActive(false);
			PlayerHUD.SetActive(true);
			Cursor.lockState = CursorLockMode.Locked;
		}
		else if (inventoryPanel.activeInHierarchy)
		{
			toggleInventory.Raise();
			Cursor.lockState = CursorLockMode.Locked;
			return;
		}
		else
		{
			pauseMenu.SetActive(true);
			PlayerHUD.SetActive(false);
			Cursor.lockState = CursorLockMode.Confined;
		}

		togglePauseEvent.Raise();
	}
}
