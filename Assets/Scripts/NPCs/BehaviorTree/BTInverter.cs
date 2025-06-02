using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A decorator node that turns the child node from a success to a failure and vice versa. Can only have one child.
/// </summary>

[CreateAssetMenu(menuName = "BehaviorTree/Decorator/Inverter")]
public class BTInverter : BTNode
{
	private BTNode Child;
	public override void Initialize(Vector2 position)
	{
		base.Initialize(position);
		Children = new List<BTNode>();
	}

	public void Attach(BTNode node)
	{
		Children.Clear();
		Children.Add(node);
		Child = node;
	}

	public override NodeState Tick()
	{
		var result = Child.Tick();
		if (result == NodeState.SUCCESS) return NodeState.FAILURE;
		if (result == NodeState.FAILURE) return NodeState.SUCCESS;
		return NodeState.RUNNING;
	}

}
