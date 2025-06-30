using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionalEventTrigger<TEvent, TData> : ConditionalEventTriggerBase
	where TEvent : GameEvent<TData>
{
	public TEvent eventToTrigger;
	public TData dataToSend;

	protected override void TriggerEvent()
	{
		eventToTrigger.Raise(dataToSend);
	}
}
