using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEventListener : GameEventListener<GameObject>
{
	public override void OnEventRaised(GameObject value)
	{
		if(value == gameObject) //Only do the responce if the listener is attached to the specific game object
		{
			base.OnEventRaised(value);
		}
	}	
}
