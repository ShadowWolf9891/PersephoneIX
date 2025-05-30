using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Selector runs it's children in order until one succeeds.
/// </summary>
public class BTSelector : BTNode
{
	private readonly List<BTNode> children;

	public BTSelector(List<BTNode> children) => this.children = children;

	public override NodeState Tick()
	{
		foreach (var child in children)
		{
			var result = child.Tick();
			if (result != NodeState.FAILURE)
				return result;
		}
		return NodeState.FAILURE;
	}
}
