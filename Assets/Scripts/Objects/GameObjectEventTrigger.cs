using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The default game event that only cares about a game object
/// This annoyingly needs its own class for unity to recognize it as a scriptable object.
/// </summary>
[CreateAssetMenu(menuName = "Events/GameObjectEventTrigger")]
public class GameObjectEventTrigger : GameEvent<GameObject> { }
