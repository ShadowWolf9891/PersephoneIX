using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// Base class for all behavior tree nodes.
/// </summary>
public abstract class BTNode : ScriptableObject
{
	[SerializeField] private BTBlackboard blackboard; //A reference to the objects we care about
	
	public static float DefaultWidth = 120f;
	public static float DefaultHeight = 60;

	public string Name = "";
	public Rect NodeRect;

	public NodePort InputPort;
	public NodePort OutputPort;

	public BTNode Parent;
	public List<BTNode> Children;

	private NodeState CurState = NodeState.IDLE;
	
	/// <summary>
	/// Initialize the Node at a specific location in the editor.
	/// </summary>
	/// <param name="position"></param>
	public virtual void Initialize(Vector2 position, BTBlackboard bb)
	{
		blackboard = bb;
		NodeRect = new Rect(position, new Vector2(DefaultWidth, DefaultHeight));
	}

	public virtual NodeState Tick(GameObject context)
	{
		CurState = Execute(context);
		return CurState;
	}

	/// <summary>
	/// What happens when this node is Ticked. Returns either running, success, or failure.
	/// </summary>
	/// <returns></returns>
	public abstract NodeState Execute(GameObject context);

	public virtual void Draw(Vector2 viewOffset)
	{
		var adjustedRect = new Rect(NodeRect.position + viewOffset, NodeRect.size);
		Color originalColor = GUI.color;

		switch (CurState)
		{
			case NodeState.RUNNING:
				GUI.color = Color.yellow;
				break;
			case NodeState.SUCCESS:
				GUI.color = Color.green;
				break;
			case NodeState.FAILURE:
				GUI.color = Color.red;
				break;
			default:
				GUI.color = Color.white;
				break;
		}
		GUI.Box(adjustedRect, Name);
		GUI.color = originalColor;
	}
	public void ResetStatus()
	{
		CurState = NodeState.IDLE;
		foreach (var child in Children)
		{
			child.ResetStatus();
		}
	}
	public virtual void Move(Vector2 position)
	{
		NodeRect.position = position;
	}

	/// <summary>
	/// Useful for if you are building the tree first then injecting the blackboard later.
	/// </summary>
	/// <param name="blackboard"></param>
	public void SetBlackboard(BTBlackboard blackboard)
	{
		this.blackboard = blackboard;
	}
	public BTBlackboard GetBlackboard() => blackboard;
}
/// <summary>
/// The state of the current node.
/// </summary>
public enum NodeState {IDLE, RUNNING, SUCCESS, FAILURE}