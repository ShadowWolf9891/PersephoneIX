using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objective/Objective Data")]
public class ObjectiveData : ScriptableObject
{
	public string MainIdentifierName = "DefaultName";
	public string MainObjectiveText = "DisplayText";

	public List<SubObjective> SubObjectives = new List<SubObjective>();
}

[Serializable]
public struct SubObjective
{ 
	public string Name;
	public string ObjectiveText;
	public Vector3 WaypointPosition;
}
