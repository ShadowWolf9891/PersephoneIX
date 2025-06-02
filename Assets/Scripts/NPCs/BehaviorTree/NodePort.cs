using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public enum PortType { INPUT, OUTPUT }

public class NodePort
{
	public BTNode ParentNode { get; private set; }
	public Rect Rect { get; private set; }
	public PortType PType { get; private set; }
	public List<NodePort> ConnectedPorts { get; set; }
	public string Label { get; private set; }

	private const float Size = 10f;
	Texture2D tex;
	public NodePort(BTNode parent, PortType type, string label = "")
	{
		ConnectedPorts = new List<NodePort>();
		ParentNode = parent;
		PType = type;
		Label = label;
		Rect = new Rect(0, 0, Size, Size);
		SetPosition(parent.NodeRect.position, (parent.NodeRect.width - Size) / 2);
		tex = Resources.Load<Texture2D>("2D Sprites/UI/CircleButton"); //Load image in resource folder
	}

	public Vector2 Center => new Vector2(Rect.x + Rect.width / 2, Rect.y + Rect.height / 2);

	public bool HasConnection => ConnectedPorts.Count > 0;

	public void Draw()
	{
		if (tex != null)
			GUI.DrawTexture(Rect, tex, ScaleMode.ScaleToFit);
		else
		{
			// Fallback if texture is missing
			GUI.color = PType == PortType.INPUT ? Color.green : Color.cyan;
			GUI.Box(Rect, "");
			GUI.color = Color.white;
		}

		if(HasConnection)
		{
			Handles.BeginGUI();
			Vector2 startTangent = Center + Vector2.right * 50f;

			foreach (NodePort cPort in ConnectedPorts)
			{
				Vector2 endTangent = cPort.Center + Vector2.left * 50f;

				Handles.DrawBezier(
					Center,
					cPort.Center,
					startTangent,
					endTangent,
					Color.white,
					null,
					4f
				);

			}
			Handles.EndGUI();
		}

	}

	public bool Contains(Vector2 point) => Rect.Contains(point);

	public void SetPosition(Vector2 nodePosition, float offsetX)
	{
		float x = nodePosition.x + offsetX;
		float y = nodePosition.y + (PType == PortType.INPUT ? -Rect.height : BTNode.DefaultHeight);
		Rect = new Rect(x,y, Size, Size);
	}
}