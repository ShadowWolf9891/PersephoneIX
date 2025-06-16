using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class DoorBehavior : MonoBehaviour, IInteractable
{
	[SerializeField] bool isLocked;
	[SerializeField] InputActionReference interactAction;

	[SerializeField] GameObject[] attachedLights;
	[SerializeField] Material LockedLightMaterial;
	[SerializeField] Material UnlockedLightMaterial;

	private NavMeshObstacle doorObstacle;

	private void Start()
	{
		doorObstacle = GetComponent<NavMeshObstacle>();
		if(isLocked)
		{
			foreach (GameObject light in attachedLights)
			{
				light.GetComponent<Light>().color = Color.red;
				light.GetComponent<MeshRenderer>().material = LockedLightMaterial;
			}
		}
		else
		{
			foreach (GameObject light in attachedLights)
			{
				light.GetComponent<Light>().color = Color.green;
				light.GetComponent<MeshRenderer>().material = UnlockedLightMaterial;
			}
		}
	}

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
		doorObstacle.carving = false;
	}
	private void CloseDoor()
	{
		gameObject.transform.position -= new Vector3(0, 3, 0);
		doorObstacle.carving = true;
	}

	private void UnlockDoor()
	{
		if(isLocked) 
		{
			isLocked = false;
			foreach(GameObject light in attachedLights)
			{
				light.GetComponent<Light>().color = Color.green;
				light.GetComponent<MeshRenderer>().material = UnlockedLightMaterial;
			}
		}
	}
	private void LockDoor()
	{
		if (!isLocked)
		{
			isLocked = true;
			foreach (GameObject light in attachedLights)
			{
				light.GetComponent<Light>().color = Color.red;
				light.GetComponent<MeshRenderer>().material = LockedLightMaterial;
			}
		}
	}
	
}
