using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
	public Line[] lines;

	public AdvancedDialogueSettings advancedDialogueSettings;
	
}

[Serializable]
public struct Line
{
	public string SpeakerName;
	[TextArea] public string line;
	public AudioClip voiceLine;

	[Tooltip("For triggering events and changed when the line is triggered.")]
	public AdvancedLineSettings advancedLineSettings;

}

public enum DialogueAdvanceMode
{
	OnClick,
	OnVoiceEnd,
	AfterDelay,
	OnEventEnd
}

[Serializable]
public struct AdvancedLineSettings
{ 
	public DialogueAdvanceMode advanceMode;

	[Tooltip("Event to do when the line starts playing. i.e play animation.")]
	public UnityEvent onLineStart;

	[Tooltip("Event to do when the line starts playing and move to the next line when it is completed." +
		" AdvanceMode must be OnEventEnd. Must callback done when completed.")]
	public UnityEvent<Action> CheckOnEventEnd;

	[Tooltip("Only used for when the advance mode is AfterDelay")]
	public float delay;
}

[Serializable]
public struct AdvancedDialogueSettings
{
	[Tooltip("Event for when the player enters the dialogue.")]
	public UnityEvent OnDialogueStart;
	[Tooltip("Event for when the player exits the dialogue.")]
	public UnityEvent OnDialogueEnd;
	[Tooltip("If the dialogue can be repeated")]
	public bool isRepeatable;
	[Tooltip("If the dialogue has been completed")]
	public bool isCompleted;



}

