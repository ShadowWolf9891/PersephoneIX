using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objective/Objective Data")]
public class ObjectiveData : ScriptableObject
{
	public string IdentifierName = "DefaultName";
	public string ObjectiveText = "DisplayText";
}
