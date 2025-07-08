using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is spawned by the Waypoint manager based on the Objective Data for the current objective. 
/// May need to make a more robust trigger system, like only go to the next waypoint if a specific event has occured.
/// </summary>
public class SpawnedWaypoint : MonoBehaviour
{
	private WaypointManager waypointManager;

	private void Start()
	{
		waypointManager = GameObject.FindFirstObjectByType<WaypointManager>();
	}
	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			Debug.Log("Triggered Collision");
			waypointManager.WaypointReached();
			gameObject.SetActive(false);
		}
	}
}
