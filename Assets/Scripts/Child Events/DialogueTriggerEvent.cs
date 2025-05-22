using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This annoyingly needs its own class for unity to recognize it as a scriptable object.
/// </summary>
[CreateAssetMenu(menuName = "Events/DialogueTriggerEvent")]
public class DialogueTriggerEvent : GameEvent<DialogueTriggerData>{}