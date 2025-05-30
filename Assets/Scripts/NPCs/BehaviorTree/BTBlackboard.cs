using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A blackboard holds all of the neccisary references a behavior tree node may need. Typically you have 1 for each AI or NPC.
/// This version is dynamic, allowing for any type of object to be store, but it is not type safe, meaning if used incorrectly
/// it will break. Make sure you know what kind of data you are trying to get, and try cast to make sure it is the correct type.
/// </summary>
public class BTBlackboard
{
	private Dictionary<string, object> data = new Dictionary<string, object>();

	public void Set<T>(string key, T value) => data[key] = value;
	public T Get<T>(string key) => data.ContainsKey(key) ? (T)data[key] : default;
}
