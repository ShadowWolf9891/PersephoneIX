using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A decorator node that turns the child node from a success to a failure and vice versa. Can only have one child.
/// </summary>

[CreateAssetMenu(menuName = "BehaviorTree/Decorator/Inverter"), Serializable]
public class BTInverter : BTDecorator
{
	public override void Initialize(Vector2 position, BTBlackboard bb)
	{
		base.Initialize(position, bb);
		Children = new List<BTNode>();
		InputPort = new NodePort(this, PortType.INPUT);
		OutputPort = new NodePort(this, PortType.OUTPUT);
	}
	public override void Draw(Vector2 viewOffset)
	{
		base.Draw(viewOffset);
		InputPort?.Draw(viewOffset);
		OutputPort?.Draw(viewOffset);
	}
	public override void Move(Vector2 position)
	{
		base.Move(position);
		InputPort?.SetPosition(position, (NodeRect.width - InputPort.Rect.width) / 2);
		OutputPort?.SetPosition(position, (NodeRect.width - OutputPort.Rect.width) / 2);
	}

	public override NodeState Execute(GameObject context)
	{
		var result = Children[0].Tick(context);
		if (result == NodeState.SUCCESS) return NodeState.FAILURE;
		if (result == NodeState.FAILURE) return NodeState.SUCCESS;
		return NodeState.RUNNING;
	}

}
