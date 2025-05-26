using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to a trigger collision volume to play dialogue.
/// </summary>
public class DialogueEventTrigger : MonoBehaviour
{
	[SerializeField, Tooltip("The type of event to trigger. Should always be DialogueTriggerEvent.")] 
	private DialogueTriggerEvent trigger;
	[SerializeField, Tooltip("The dialogue data that you want to play when the trigger occurs.")] 
	private DialogueTriggerData dialogueDataToLoad;
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			trigger.Raise(dialogueDataToLoad);
		}
	}
}
