using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all behavior tree nodes.
/// </summary>
public abstract class BTNode
{
	protected BTBlackboard blackboard; //A reference to a dictionary of all the objects we care about.

	/// <summary>
	/// Defualt constructor for when this node does not care about any other objects in the game.
	/// </summary>
	protected BTNode() { }
	/// <summary>
	/// Constructor with a blackboard reference if this node cares about anything that happens in the game. i.e. where the player is.
	/// </summary>
	/// <param name="blackboard"></param>
	protected BTNode(BTBlackboard blackboard) 
	{
		this.blackboard = blackboard;
	}
	/// <summary>
	/// What happens when this node is Ticked. Returns either running, success, or failure.
	/// </summary>
	/// <returns></returns>
	public abstract NodeState Tick();

	/// <summary>
	/// Useful for if you are building the tree first then injecting the blackboard later.
	/// </summary>
	/// <param name="blackboard"></param>
	public void SetBlackboard(BTBlackboard blackboard)
	{
		this.blackboard = blackboard;
	}
}
/// <summary>
/// The state of the current node.
/// </summary>
public enum NodeState {RUNNING, SUCCESS, FAILURE}