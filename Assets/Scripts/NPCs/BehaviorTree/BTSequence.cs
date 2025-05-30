using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A sequence node runs its children in order until it fails.
/// </summary>
public class BTSequence : BTNode
{
	private readonly List<BTNode> children;

	public BTSequence(List<BTNode> children) => this.children = children;

	public override NodeState Tick()
	{
		foreach (var child in children)
		{
			var result = child.Tick();
			if (result != NodeState.SUCCESS)
				return result;
		}
		return NodeState.SUCCESS;
	}
}
