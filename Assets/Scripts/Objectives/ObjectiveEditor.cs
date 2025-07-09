#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class ObjectiveEditor : MonoBehaviour
{
	public ObjectiveData objectiveData;

	[ContextMenu("Capture SubObjective Waypoints From Children")]
	public void CaptureSubObjectives()
	{
		if (objectiveData == null)
		{
			Debug.LogWarning("No ObjectiveData assigned.");
			return;
		}

		objectiveData.SubObjectives.Clear();

		foreach (Transform child in transform)
		{
			SubObjective sub = new SubObjective
			{
				Name = child.name,
				ObjectiveText = child.name,
				WaypointPosition = child.position
			};

			objectiveData.SubObjectives.Add(sub);
		}

		EditorUtility.SetDirty(objectiveData);
		AssetDatabase.SaveAssets();

		Debug.Log("Captured " + objectiveData.SubObjectives.Count + " sub-objective positions.");
	}
}
#endif