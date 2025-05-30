using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A "leaf" node that performs actions and checks
/// </summary>
public abstract class BTActionNode : BTNode
{
	private readonly Func<NodeState> action;

	public BTActionNode(Func<NodeState> action) => this.action = action;

	public override NodeState Tick() => action();
}
