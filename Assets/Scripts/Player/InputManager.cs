using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Controls what happens when the player interects with the mouse or keyboard.
/// </summary>
public class InputManager : MonoBehaviour
{
	GameObject player;
	PlayerInput pInput;
	MovePlayer _MovePlayer;
	[SerializeField]GameEvent playerInteract;

	public static InputManager Instance { get; private set; }


	private void Awake()
	{
		// Ensure only one instance exists
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject); // Persist across scenes

		//Try and get the player and attached components
		player = GameObject.FindGameObjectWithTag("Player");
		if (player == null)
		{
			Debug.LogError("Failed to find player! Make sure the player has the " +
				"'player' tag and exists in the world at runtime.");
		}
		if(!player.TryGetComponent(out pInput))
		{
			Debug.LogError("Failed to find player input component attached to the player!");
		}
		if (!player.TryGetComponent(out _MovePlayer))
		{
			Debug.LogError("Failed to find move player component attached to the player!");
		}
	}

	public void OnMove(CallbackContext context)
    {
		_MovePlayer.SetInputVector(context.ReadValue<Vector2>());
    }

	public void OnLook(CallbackContext context)
	{
		_MovePlayer.SetLookVector(context.ReadValue<Vector2>());
	}

	/// <summary>
	/// Occurs when the player presses the interact key. (Currently 'E')
	/// </summary>
	/// <param name="context"></param>
	public void Interact(CallbackContext context) 
	{
		if (context.started)
		{
			playerInteract.Raise();
		}
	}
}
