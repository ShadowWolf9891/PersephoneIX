using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A decorator node that turns the child node from a success to a failure and vice versa. Can only have one child.
/// </summary>
public class BTInverter : BTNode
{
	private readonly BTNode child;

	public BTInverter(BTNode child) => this.child = child;

	public override NodeState Tick()
	{
		var result = child.Tick();
		if (result == NodeState.SUCCESS) return NodeState.FAILURE;
		if (result == NodeState.FAILURE) return NodeState.SUCCESS;
		return NodeState.RUNNING;
	}

}
