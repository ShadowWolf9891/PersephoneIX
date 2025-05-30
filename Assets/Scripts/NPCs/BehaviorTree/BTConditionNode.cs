using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A condition node checks if a condition is true and returns either success or failure.
/// </summary>
public abstract class BTConditionNode : BTNode
{
	private readonly Func<bool> condition;

	public BTConditionNode(Func<bool> condition) => this.condition = condition;

	public override NodeState Tick()
	{
		return condition() ? NodeState.SUCCESS : NodeState.FAILURE;
	}
}
