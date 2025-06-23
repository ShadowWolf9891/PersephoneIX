using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveStartEvent : MonoBehaviour
{
	[SerializeField, Tooltip("The type of event to trigger. Should always be ObjectiveTriggerEvent.")]
	private ObjectiveTriggerEvent trigger;
	[SerializeField, Tooltip("The objective data that you want to load when the trigger occurs.")]
	private ObjectiveData objectiveDataToLoad;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			trigger.Raise(objectiveDataToLoad);
			gameObject.SetActive(false);
		}
	}
}
