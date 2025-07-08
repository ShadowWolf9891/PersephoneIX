using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour, IGameEventListener<ObjectiveData>
{
	[SerializeField] TextMeshProUGUI objectiveText;
	[SerializeField] GameObject objectivePanel;
	[SerializeField] GameObject newObjectivePanel;
	[SerializeField] GameObject waypointPrefab;
	[SerializeField] WaypointManager waypointManager;

	private float hideDelay = 3f;
	private List<ObjectiveData> completedObjectives = new(); //Write to disk when making a save system.
	ObjectiveData currentObjective;

	public void OnEventRaised(ObjectiveData data)
	{
		Debug.Log("Objective Event Raised");
		if(completedObjectives.Contains(data)) {return; }

		if (currentObjective != data) StartObjective(data);
		else if(waypointManager.isComplete) //If each step of the objective is completed.
		{
			EndObjective(data);
		}
	}
	private void StartObjective(ObjectiveData data)
	{	
		currentObjective = data;
		objectiveText.text = data.ObjectiveText;
		newObjectivePanel.SetActive(true);
		objectivePanel.SetActive(true);
		SpawnWaypoints(data);
		StartCoroutine(HideAfterDelay());
	}
	/// <summary>
	/// Spawn waypoints based on the objective data at specific locations. 
	/// Consider making the data a scriptable object with more information, not just a position.
	/// </summary>
	/// <param name="data"></param>
	private void SpawnWaypoints(ObjectiveData data)
	{
		List<GameObject> waypoints = new List<GameObject>();
		foreach(Vector3 pos in data.WaypointPositions)
		{
			waypoints.Add(Instantiate(waypointPrefab, pos, Quaternion.identity));
			Debug.Log("Spawned Waypoint");
		}
		waypointManager.SetWaypoints(waypoints);
	}

	private void EndObjective(ObjectiveData data)
	{
		currentObjective = null;
		completedObjectives.Add(data);
		objectivePanel.SetActive(false);
	}
	private IEnumerator HideAfterDelay()
	{
		yield return new WaitForSeconds(hideDelay);
		newObjectivePanel.SetActive(false);
	}
}
