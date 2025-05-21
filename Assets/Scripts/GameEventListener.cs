using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Attach this script to objects that react to an event. 
/// It will invoke the specific UnityEvent when the event is raised and can take in arguments.
/// </summary>
public abstract class GameEventListener<T> : MonoBehaviour, IGameEventListener<T>
{
	[SerializeField, Tooltip("The event to listen for.")] private GameEvent<T> gameEvent;
	[SerializeField, Tooltip("The event to execute when gameEvent is fired.")] private UnityEvent<T> response;

	private void OnEnable() => gameEvent.RegisterListener(this);
	private void OnDisable() => gameEvent.UnregisterListener(this);

	public void OnEventRaised(T value) => response.Invoke(value);
}

public class GameEventListener : GameEventListener<Unit>
{
	// Uses UnityEvent with no parameters — works out of the box in Inspector
}
