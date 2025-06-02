using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior tree class that contains the root node on creation. Behavior trees are fairly complex, so I suggest researching them
/// if you plan on using them to control AI or NPC actions.
/// </summary>
[CreateAssetMenu(menuName = "BehaviorTree/BehaviorTree")]
public class BehaviorTree : ScriptableObject
{
	public BTNode rootNode;
	public List<BTNode> nodes = new();

	public void AddNode(BTNode node)
	{
		nodes.Add(node);
		if(node.Name == "")
		{
			node.Name = node.GetType().Name;
		}
		node.name = node.Name;
	}
	public void RemoveNode(BTNode node)
	{
		nodes.Remove(node);
	}

	/// <summary>
	/// Send a tick down the chain of the behavior tree.
	/// </summary>
	public NodeState Tick()
    {
		return rootNode != null ? rootNode.Tick() : NodeState.FAILURE;
	}
}
