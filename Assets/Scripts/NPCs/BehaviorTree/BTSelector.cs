using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Selector runs it's children in order until one succeeds.
/// </summary>
[CreateAssetMenu(menuName = "BehaviorTree/Composite/Selector")]
public class BTSelector : BTNode
{
	public override void Initialize(Vector2 position)
	{
		base.Initialize(position);
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
	public override NodeState Tick()
	{
		foreach (var child in Children)
		{
			var result = child.Tick();
			if (result != NodeState.FAILURE)
				return result;
		}
		return NodeState.FAILURE;
	}
}
