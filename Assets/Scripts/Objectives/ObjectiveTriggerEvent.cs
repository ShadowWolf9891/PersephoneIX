using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This annoyingly needs its own class for unity to recognize it as a scriptable object.
/// </summary>
[CreateAssetMenu(menuName = "Events/ObjectiveTriggerEvent")]
public class ObjectiveTriggerEvent : GameEvent<ObjectiveData> { }
