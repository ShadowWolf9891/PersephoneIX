using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// Execute an event "functionName" in class "target". 
/// </summary>
[Serializable]
public class BTActionNode : BTNode
{
	[Tooltip("The inspector name of the object you want to execute the function of.")]
	public string targetName;
	[Tooltip("The name of the function you want to execute.")]
	public string functionName;
	public override void Initialize(Vector2 position, BTBlackboard bb)
	{
		base.Initialize(position, bb);
		Children = new List<BTNode>();
		InputPort = new NodePort(this, PortType.INPUT);
		OutputPort = null;
	}
	public override void Draw(Vector2 viewOffset)
	{
		base.Draw(viewOffset);
		InputPort?.Draw(viewOffset);
		//OutputPort?.Draw(viewOffset);
	}
	public override void Move(Vector2 position)
	{
		base.Move(position);
		InputPort?.SetPosition(position, (NodeRect.width - InputPort.Rect.width) / 2);
		//OutputPort?.SetPosition(position, (NodeRect.width - OutputPort.Rect.width) / 2);
	}
	public override NodeState Execute(GameObject context)
	{
		if (context == null || string.IsNullOrEmpty(functionName))
			return NodeState.FAILURE;

		var components = context.GetComponents<MonoBehaviour>();
		foreach (var comp in components)
		{
			var method = comp.GetType().GetMethod(functionName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
			if (method != null)
			{
				method.Invoke(comp, null);
				return NodeState.SUCCESS;
			}
		}

		Debug.LogWarning($"Method '{functionName}' not found on any MonoBehaviour attached to {context.name}");
		return NodeState.FAILURE;
	}
}
 