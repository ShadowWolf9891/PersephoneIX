using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConsoleBehavior : MonoBehaviour, IInteractable
{
	[SerializeField] InputActionReference interactAction;
	[SerializeField] GameObjectEventTrigger eventToTrigger;
	[SerializeField] GameObject DoorToUnlock;
	public string GetInteractionPrompt()
	{
		return $"Press {interactAction.action.GetBindingDisplayString()}";
	}

	public bool IsHoldInteraction()
	{
		return false;
	}

	public void OnInteractStart()
	{
		if (DoorToUnlock != null)
		{
			eventToTrigger.Raise(DoorToUnlock);
		}
		
	}

	public void OnInteractStop()
	{
		throw new System.NotImplementedException();
	}

	
}
