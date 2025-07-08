using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Move from one waypoint to the next when the previous one is reached. There should only be one Waypoint Manager.
/// </summary>
public class WaypointManager : MonoBehaviour 
{
	[SerializeField] WaypointUI WaypointUI;
	public bool isComplete { get; private set; }

	private int currentWaypointIndex = 0;

	List<GameObject> waypoints;

	public void SetWaypoints(List<GameObject> waypoints)
	{
		this.waypoints = waypoints;
		isComplete = false;
		SetActiveWaypoint(currentWaypointIndex);
	}

	public void WaypointReached()
	{
		currentWaypointIndex++;
		if (currentWaypointIndex < waypoints.Count)
			SetActiveWaypoint(currentWaypointIndex);
		else
		{
			isComplete = true;
			currentWaypointIndex = 0;
		}
			

		Debug.Log("Reached Waypoint");
	}

	void SetActiveWaypoint(int index)
	{
		for (int i = 0; i < waypoints.Count; i++)
			waypoints[i].gameObject.SetActive(i == index);

		WaypointUI.ChangeTarget(waypoints[index]);
	}
}
