using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConsoleBehavior : MonoBehaviour, IInteractable
{
	[SerializeField] InputActionReference interactAction;
	[SerializeField] GameObjectEventTrigger eventToTrigger;
	[SerializeField] GameObject DoorToUnlock;
	[SerializeField] bool OneTime = true;

	bool doneAction = false;
	public string GetInteractionPrompt()
	{
		if (doneAction && OneTime) return "";

		if(DoorToUnlock != null && eventToTrigger != null) { return $"Press {interactAction.action.GetBindingDisplayString()}";}

		return "";
		
	}

	public bool IsHoldInteraction()
	{
		return false;
	}

	public void OnInteractStart()
	{
		if(doneAction && OneTime) return;

		if (DoorToUnlock != null)
		{
			eventToTrigger.Raise(DoorToUnlock);
			doneAction = true;
		}
		
	}

	public void OnInteractStop()
	{
		throw new System.NotImplementedException();
	}

	
}
