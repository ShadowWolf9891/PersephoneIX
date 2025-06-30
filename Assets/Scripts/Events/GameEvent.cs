using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game event is a reusable scriptable object that can be reused for many system wide events.
/// 1. Create new events in the Project window via Assets > Create > Events > Game Event.
/// 2. Attach GameEventListener class to objects that react to that event.
/// 3. Drag a GameEvent into the gameEvent field in the Inspector of the GameEventListener. 
///		Hook up any function to response - e.g. OpenDoor(), PlayAlarm().
/// 4. Trigger the event from any script using yourGameEventName.Raise()
/// 
/// For new events that pass in data:
/// 1. Create a new class [YourClassName] : GameEvent[YourDataType]
/// 2. In the editor run Tool > Generate Game Event Classes. This will create scripts that can be attached to gameobjects.
/// 3. Add Component > Events > Triggers and Add Component > Events > Listeners to game objects and follow above steps 1 - 4.
/// 
/// </summary>

public abstract class GameEvent<T> : ScriptableObject
{
    private readonly List<IGameEventListener<T>> listeners = new List<IGameEventListener<T>> ();

	/// <summary>
	/// Call this and pass in any value to send it to the event that is triggered.
	/// </summary>
	/// <param name="value"></param>
    public void Raise(T value)
    {
		EventHistory.Instance.MarkRaised(this);
		Debug.Log($"Raised event with type {GetType()}");
		for (int i = listeners.Count - 1; i >= 0; i--)
		{
			listeners[i].OnEventRaised(value);
		}
	}

	/// <summary>
	/// Call this to subscribe to this event.
	/// </summary>
	/// <param name="listener"></param>
	public void RegisterListener(IGameEventListener<T> listener)
	{
		if (!listeners.Contains(listener))
			listeners.Add(listener);
	}
	/// <summary>
	/// Call this to unsubscribe from this event.
	/// </summary>
	/// <param name="listener"></param>
	public void UnregisterListener(IGameEventListener<T> listener)
	{
		if (listeners.Contains(listener))
			listeners.Remove(listener);
	}

}
/// <summary>
/// Interface for generic event types
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGameEventListener<T>
{ 
	void OnEventRaised(T value);
}
/// <summary>
/// Struct for passing a "void" variable for events that don't pass in arguments
/// </summary>
[System.Serializable]
public readonly struct Unit
{
	public static readonly Unit Default = new Unit();
}

//Here are the different types of events that can be created

[CreateAssetMenu(menuName = "Events/DefaultGameEvent")]
public class GameEvent : GameEvent<Unit>
{
	public void Raise() => Raise(Unit.Default);
}
