using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventTrigger : MonoBehaviour
{
	[SerializeField] private DialogueTriggerEvent dte;
	[SerializeField] private DialogueTriggerData dialogueDataToLoad;
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			dte.Raise(dialogueDataToLoad);
		}
	}
}
