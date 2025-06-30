using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHistory
{
	//Create a static instance
	private static EventHistory _instance;
	public static EventHistory Instance => _instance ??= new EventHistory();

	//Store events that have been raised and events that are successful
	private readonly HashSet<ScriptableObject> raisedEvents = new();
	private readonly HashSet<ScriptableObject> successfulEvents = new();

	private EventHistory() { } // private constructor

	public void MarkRaised(ScriptableObject evt) => raisedEvents.Add(evt);
	public void MarkSuccessful(ScriptableObject evt) => successfulEvents.Add(evt);

	public bool WasRaised(ScriptableObject evt) => raisedEvents.Contains(evt);
	public bool WasSuccessful(ScriptableObject evt) => successfulEvents.Contains(evt);
	
}
