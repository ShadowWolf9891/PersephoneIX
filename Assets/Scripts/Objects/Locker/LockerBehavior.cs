using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockerBehavior : MonoBehaviour, IInteractable
{
	[SerializeField] InputActionReference interactAction;
	[SerializeField] GameObject lockerDoor1;
    [SerializeField] GameObject lockerDoor2;

	bool playerInside = false;
	bool doorsOpen = false;

	public string GetInteractionPrompt()
	{
		if(doorsOpen) return $"Press {interactAction.action.GetBindingDisplayString()} to close door";
		return $"Press {interactAction.action.GetBindingDisplayString()} to open door";
	}

	public bool IsHoldInteraction()
	{
		return false;
	}

	public void OnInteractStart()
	{
		if(doorsOpen ) 
		{
			CloseDoors();
			doorsOpen = false;
		}
		else
		{
			OpenDoors();
			doorsOpen= true;
		}
	}

	public void OnInteractStop()
	{
		throw new System.NotImplementedException();
	}

	public void OpenDoors()
	{
		lockerDoor1.transform.Rotate(0,120,0);
		lockerDoor2.transform.Rotate(0, -120, 0);
	}

	public void CloseDoors() 
	{
		lockerDoor1.transform.Rotate(0, -120, 0);
		lockerDoor2.transform.Rotate(0, 120, 0);
	}


}
