using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class DoorBehavior : MonoBehaviour, IInteractable
{
	[SerializeField] bool isLocked;
	[SerializeField] InputActionReference interactAction;
	public string GetInteractionPrompt()
	{
		if (isLocked)
		{
			return "Locked";
		}
		else
		{
			//This returns both binding keys. Need to use groups and detect last used input.
			return $"Press {interactAction.action.GetBindingDisplayString()}";
		}
		
	}

	public bool IsHoldInteraction()
	{
		return false;
	}

	public void OnInteractStart()
	{
		if(!isLocked) 
		{
			OpenDoor();
		}
	}
	public void OnInteractStop()
	{
		throw new NotImplementedException();
	}
	private void OpenDoor()
	{
		gameObject.transform.position += new Vector3(0,3,0);
	}
	private void CloseDoor()
	{
		gameObject.transform.position -= new Vector3(0, 3, 0);
	}

	
}
