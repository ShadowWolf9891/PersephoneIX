using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// A sequence node runs its children in order until it fails.
/// </summary>
[CreateAssetMenu(menuName = "BehaviorTree/Composite/Sequence"), Serializable]
public class BTSequence : BTNode
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
		foreach (var child in Children)
		{
			var result = child.Tick(context);
			if (result != NodeState.SUCCESS)
				return result;
		}

		return NodeState.SUCCESS;
	}
}
