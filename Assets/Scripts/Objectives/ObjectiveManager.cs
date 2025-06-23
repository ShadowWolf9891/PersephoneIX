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

	private float hideDelay = 3f;
	private List<ObjectiveData> completedObjectives = new(); //Write to disk when making a save system.
	ObjectiveData currentObjective;

	public void OnEventRaised(ObjectiveData data)
	{
		Debug.Log("Objective Event Raised");
		if(completedObjectives.Contains(data)) {return; }

		if (currentObjective != data) StartObjective(data);
		else
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

		StartCoroutine(HideAfterDelay());
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
