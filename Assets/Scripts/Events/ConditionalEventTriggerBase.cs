using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for conditional event triggers. Use ConditionalEventTrigger to trigger an event if every event on the list has been raised.
/// </summary>
public abstract class ConditionalEventTriggerBase : MonoBehaviour
{
	public List<GameEvent> requiredEvents;
	void Update()
	{
		if (AllRequirementsMet())
		{
			TriggerEvent();
			enabled = false;
		}
	}

	private bool AllRequirementsMet()
	{
		foreach (var e in requiredEvents)
		{
			if (!EventHistory.Instance.WasRaised(e))
				return false;
		}
		return true;
	}

	protected abstract void TriggerEvent();
}
