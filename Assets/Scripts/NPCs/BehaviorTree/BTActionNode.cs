using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// A "leaf" node that performs actions and checks. Currently just keeps things organized and allows opperations on all Action nodes.
/// Action Nodes do not have children.
/// </summary>
public abstract class BTActionNode : BTNode
{
	//public override NodeState Tick()
	//{
		//Do something
	//	return result (Success / failure / runnning)
	//}
}
