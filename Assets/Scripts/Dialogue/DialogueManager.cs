using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DialogueManager : MonoBehaviour, IGameEventListener<DialogueTriggerData>
{
	[SerializeField] GameObject dialoguePanel;
	[SerializeField] TextMeshProUGUI dialogueBoxText;
	private HashSet<DialogueData> completedDialogues = new(); //Stores the dialogue that has been completed

	private Coroutine delayCoroutine; //To make sure only one delay is running at a time.
	DialogueData currentData;
	int currentLineIndex = 0;
	

	public void OnEventRaised(DialogueTriggerData data)
	{
		Debug.Log("Event Raised");
		StartDialogue(data.dialogue);
	}

	private void StartDialogue(DialogueData data)
	{
		Debug.Log("Started Dialogue");
		dialoguePanel.SetActive(true);
		currentData = data;
		currentLineIndex = 0;
		ShowCurrentLine();
	}

	private void ShowCurrentLine()
	{
		if (currentLineIndex >= currentData.lines.Length)
		{
			EndDialogue();
			return;
		}

		Line lineData = currentData.lines[currentLineIndex];

		//Display line and play audio
		dialogueBoxText.text = lineData.line;
		//Play audio here

		//Play events that happen when the line begins
		lineData.advancedLineSettings.onLineStart?.Invoke();

		switch (lineData.advancedLineSettings.advanceMode)
		{
			case DialogueAdvanceMode.AfterDelay:
				if(delayCoroutine != null) StopCoroutine(delayCoroutine); //Stop previous delay coroutine first
				delayCoroutine = StartCoroutine(AdvanceAfterDelay(lineData.advancedLineSettings.delay));
				break;
			case DialogueAdvanceMode.OnEventEnd:
				WaitForEvent(lineData);
				break;
			case DialogueAdvanceMode.OnVoiceEnd:
				//Wait for voice line to finish here
				break;

			default:
				break;
		}
	}

	private void EndDialogue()
	{
		MarkComplete(currentData);
		currentData = null;
		dialoguePanel.SetActive(false);
		Debug.Log("Ended Dialogue");
	}

	private IEnumerator AdvanceAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		AdvanceToNextLine();
	}

	/// <summary>
	/// Wait for an event to finish before going to the next line. May not work for multiple events.
	/// </summary>
	/// <param name="lineData"></param>
	/// <returns></returns>
	public IEnumerator WaitForEvent(Line lineData)
	{
		bool done = false;

		lineData.advancedLineSettings.CheckOnEventEnd?.Invoke(() => {
			done = true;
		});

		// Wait until something calls the callback
		yield return new WaitUntil(() => done);

		AdvanceToNextLine();
	}

	/// <summary>
	/// Called when the player interacts
	/// </summary>
	public void WaitForPlayerInput()
	{
		if (dialoguePanel.activeInHierarchy)
		{
			if (currentData.lines[currentLineIndex].advancedLineSettings.advanceMode == DialogueAdvanceMode.OnClick)
			{
				AdvanceToNextLine();
			}
		}
	}

	private void AdvanceToNextLine()
	{
		currentLineIndex++;
		ShowCurrentLine();
	}
	public void MarkComplete(DialogueData data)
	{
		completedDialogues.Add(data);
	}

	public bool IsCompleted(DialogueData data)
	{
		return completedDialogues.Contains(data);
	}
}

[System.Serializable]
public class DialogueTriggerData
{
	public DialogueData dialogue;
}