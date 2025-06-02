using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A condition node checks if a condition is true and returns either success or failure. This class currently keeps things organized
/// and allows for condition nodes to be grouped together. Condition nodes do not have children.
/// </summary>
public abstract class BTConditionNode : BTNode
{
	//Example implementation

	//public override NodeState Tick()
	//{
	//	return condition() ? NodeState.SUCCESS : NodeState.FAILURE;
	//}
}
