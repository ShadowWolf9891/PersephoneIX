using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Display the current waypoint on the player HUD.
/// </summary>
public class WaypointUI : MonoBehaviour
{
	[SerializeField] Transform curTarget; //World space location to track.
	[SerializeField] RectTransform icon;
	[SerializeField] TextMeshProUGUI distanceNum;
	private GameObject player;
	private Camera playerCam;

	private void Awake()
	{
		if (distanceNum == null)
			distanceNum = GetComponentInChildren<TextMeshProUGUI>();
	}

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerCam = Camera.main;
	}

	private void Update()
	{
        if (gameObject.activeInHierarchy && curTarget)
		{
			distanceNum.text = $"{UpdateDistance()}m";
		
			Vector3 screenPos = playerCam.WorldToScreenPoint(curTarget.position);
			// Hide if behind camera
			icon.gameObject.SetActive(screenPos.z > 0);
			icon.position = screenPos;
		}
		
	}

	private int UpdateDistance()
	{
		if(!curTarget) return -1 ;
		return (int)Vector3.Distance(player.transform.position, curTarget.transform.position);
	}

	public void ChangeTarget(GameObject target)
	{
		curTarget = target.transform;
	}
}
