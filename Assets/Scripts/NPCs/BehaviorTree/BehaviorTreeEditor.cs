using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class BehaviorTreeEditor : EditorWindow
{
    private BehaviorTree tree;
	private Editor nodeEditor;
	private BTNode selectedNode;
	private NodePort selectedPort;
	private bool movingNode;
	private bool drawingConnection;
	Vector2 curOffset = Vector2.zero;
	Vector2 viewOffset = Vector2.zero;
	

	[MenuItem("Window/Behavior Tree Editor")]
	static void OpenWindow()
	{
		BehaviorTreeEditor window = GetWindow<BehaviorTreeEditor>("Behavior Tree");
		window.Show();
	}
	private void OnGUI()
	{
		Rect graphRect = new Rect(0, 0, position.width * 0.7f, position.height); // 70% width for graph
		Rect inspectorRect = new Rect(graphRect.width, 0, position.width - graphRect.width, position.height); // Remaining 30%

		
		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		if (GUILayout.Button("New Tree", EditorStyles.toolbarButton))
		{
			CreateNewTree();
		}
		if (GUILayout.Button("Load Tree", EditorStyles.toolbarButton))
		{
			LoadTree();
		}
		GUILayout.Space(300);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		DrawGraphArea(graphRect);
		DrawInspectorArea(inspectorRect);
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();

		if (tree != null)
		{
			ProcessEvents(Event.current, graphRect);
		}

	}

	private void DrawInspectorArea(Rect inspectorRect)
	{
		GUILayout.BeginArea(inspectorRect, EditorStyles.helpBox);
		if (nodeEditor != null)
		{
			GUILayout.Label("Node Inspector", EditorStyles.boldLabel);
			nodeEditor.OnInspectorGUI();
		}
		GUILayout.EndArea();
	}

	private void DrawGraphArea(Rect graphRect)
	{
		GUILayout.BeginArea(graphRect);
		if (tree != null)
		{
			DrawNodes();
		}
		GUILayout.EndArea();
	}

	private void CreateNewTree()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save Behavior Tree", "NewBehaviorTree", "asset", "Save your new behavior tree");
		if (string.IsNullOrEmpty(path)) { return; }

		// Create the BehaviorTree asset
		tree = ScriptableObject.CreateInstance<BehaviorTree>();

		// Create the root node (e.g. BTSequence)
		var rootNode = ScriptableObject.CreateInstance<BTSequence>();
		rootNode.Initialize(new Vector2(100,100));
		rootNode.Name = "RootNode";
		rootNode.name = rootNode.Name;
		rootNode.InputPort = null;

		// Save root node as a subasset of the tree
		AssetDatabase.CreateAsset(tree, path);
		AssetDatabase.AddObjectToAsset(rootNode, tree);

		// Assign root node and mark tree dirty (means it must be saved by editor)
		tree.rootNode = rootNode;
		EditorUtility.SetDirty(tree);

		// Save and refresh
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		Debug.Log("Created new Behavior Tree with default root node.");
	}

	private void LoadTree()
	{
		string path = EditorUtility.OpenFilePanel("Load Behavior Tree", "Assets", "asset");
		path = FileUtil.GetProjectRelativePath(path);
		tree = AssetDatabase.LoadAssetAtPath<BehaviorTree>(path);
	}

	private void DrawNodes()
	{
		tree.rootNode.Draw(viewOffset);
		DrawLines(tree.rootNode);
		foreach (BTNode node in tree.nodes)
		{
			node.Draw(viewOffset);
			DrawLines(node);
		}
	}

	private void DrawLines(BTNode node)
	{
		foreach (var child in node.Children)
		{
			DrawConnection(node.OutputPort.Center + viewOffset, child.InputPort.Center + viewOffset);
		}
	}



	/// <summary>
	/// Process GUI events like when nodes are clicked.
	/// </summary>
	/// <param name="e"></param>
	/// <param name="GraphArea"></param>
	private void ProcessEvents(Event e, Rect GraphArea)
	{
		if (e.type == EventType.ContextClick) // or e.button == 1 && e.type == MouseDown
		{
			if (ClickedOnNode(e.mousePosition, out BTNode clickedNode))
			{
				ShowNodeContextMenu(clickedNode);
				e.Use(); // Mark event as used
			}
			else if (ClickedOnPort(e.mousePosition, out NodePort clickedPort))
			{
				ShowPortContextMenu(clickedPort);
			}
			else
			{
				ShowContextMenu(e.mousePosition);
				e.Use(); // Mark event as used
			}
			
		}
		if (e.type == EventType.MouseDrag && e.button == 2) // Middle mouse button
		{
			//Change offset to reflect where things should be drawn
			viewOffset += e.delta;
			GUI.changed = true;
			e.Use();
		}

		//Select the node and display node editor.
		if (e.type == EventType.MouseDown && GraphArea.Contains(e.mousePosition) && e.button == (int)MouseButton.Left)
		{
			if (ClickedOnNode(e.mousePosition, out BTNode clickedNode))
			{
				if (nodeEditor != null)
					DestroyImmediate(nodeEditor);

				// Create a cached editor for the selected node
				nodeEditor = Editor.CreateEditor(clickedNode);
				curOffset = clickedNode.NodeRect.position - e.mousePosition;
				movingNode = true;
				selectedNode = clickedNode;
				e.Use();
			}
			else if(ClickedOnPort(e.mousePosition, out NodePort clickedPort))
			{
				selectedPort = clickedPort;
				drawingConnection = true;
			}
			else
			{
				DestroyImmediate(nodeEditor);
				nodeEditor = null;
				curOffset = Vector2.zero;
				selectedNode = null;
				e.Use();
			}
		}
		if(e.type == EventType.MouseUp && movingNode && e.button == (int)MouseButton.Left)
		{
			movingNode = false;
			curOffset = Vector2.zero;
			e.Use();
		}

		if (movingNode && e.type == EventType.MouseDrag && e.button == (int)MouseButton.Left)
		{
			selectedNode.Move(e.mousePosition + curOffset);	
			GUI.changed = true;
			e.Use();
		}

		if(drawingConnection)
		{
			DrawConnection(selectedPort.Center + viewOffset, e.mousePosition);
			GUI.changed = true;
		}

		if (e.type == EventType.MouseUp && drawingConnection && e.button == (int)MouseButton.Left)
		{
			drawingConnection = false;

			if(ClickedOnPort(e.mousePosition,out NodePort clickedPort))
			{
				if(selectedPort.PType != clickedPort.PType && selectedPort != clickedPort)
				{
					if(selectedPort.PType == PortType.OUTPUT && !selectedPort.ParentNode.Children.Contains(clickedPort.ParentNode)) 
					{
						selectedPort.ParentNode.Children.Add(clickedPort.ParentNode);
						clickedPort.ParentNode.Parent = selectedPort.ParentNode;
						
					}
					else if(selectedPort.PType == PortType.INPUT && !clickedPort.ParentNode.Children.Contains(selectedPort.ParentNode))
					{
						clickedPort.ParentNode.Children.Add(selectedPort.ParentNode);
						selectedPort.ParentNode.Parent = clickedPort.ParentNode;
					}
				}
			}
			e.Use();
		}

	}

	private void ShowContextMenu(Vector2 position)
	{
		GenericMenu menu = new GenericMenu();
		menu.AddItem(new GUIContent("Add/Sequence Node"), false, () => CreateNode<BTSequence>(position));
		menu.AddItem(new GUIContent("Add/Selector Node"), false, () => CreateNode<BTSelector>(position));
		menu.ShowAsContext();
	}
	private void ShowNodeContextMenu(BTNode node)
	{
		if(node == tree.rootNode) { return; }

		GenericMenu menu = new GenericMenu();
		menu.AddItem(new GUIContent("Delete Node"), false, () => DeleteNode(node));
		menu.ShowAsContext();
	}
	private void ShowPortContextMenu(NodePort port)
	{
		if(port.ParentNode.Children.Count > 0) 
		{
			GenericMenu menu = new GenericMenu();
			menu.AddItem(new GUIContent("Break Connection"), false, () => RemoveConnection(port));
			menu.ShowAsContext();
		}
		
	}

	private bool ClickedOnNode(Vector2 mousePosition, out BTNode clickedNode)
	{
		mousePosition -= viewOffset;
		if (tree.rootNode.NodeRect.Contains(mousePosition))
		{
			clickedNode = tree.rootNode;
			return true;
		}

		foreach (BTNode node in tree.nodes)
		{
			if (node.NodeRect.Contains(mousePosition))
			{
				clickedNode = node;
				return true;
			}
		}
		clickedNode = null;
		return false;
	}

	private bool ClickedOnPort(Vector2 mousePosition, out NodePort clickedPort)
	{
		mousePosition -= viewOffset;
		if (tree.rootNode.OutputPort.Contains(mousePosition))	
		{
			clickedPort = tree.rootNode.OutputPort;
			return true;
		}

		foreach (BTNode node in tree.nodes)
		{
			if (node.InputPort.Contains(mousePosition))
			{
				clickedPort = node.InputPort;
				return true;
			}
			if (node.OutputPort.Contains(mousePosition))
			{
				clickedPort = node.OutputPort;
				return true;
			}
		}
		clickedPort = null;
		return false;
	}

	private void CreateNode<T>(Vector2 pos) where T : BTNode
	{
		pos -= viewOffset;
		var node = ScriptableObject.CreateInstance<T>();
		node.Initialize(pos);
		tree.AddNode(node);
	}
	private void DeleteNode(BTNode node)
	{
		RemoveConnection(node.InputPort);
		RemoveConnection(node.OutputPort);

		if (selectedNode == node)
		{
			selectedNode = null;
			nodeEditor = null;
		}
		tree.RemoveNode(node);
		GUI.changed = true;
	}

	void DrawConnection(Vector2 startPos, Vector2 endPos)
	{
		Handles.BeginGUI();
		Vector2 startTangent = startPos + Vector2.right * 50f;
		Vector2 endTangent = endPos +Vector2.left * 50f;

		Handles.DrawBezier(
			startPos ,
			endPos,
			startTangent,
			endTangent,
			Color.white,
			null,
			4f
		);
		Handles.EndGUI();
	}
	void RemoveConnection(NodePort nodePort)
	{
		if(nodePort.PType == PortType.INPUT) 
		{
			BTNode child = nodePort.ParentNode;
			BTNode parent = child.Parent;

			if (parent != null)
			{
				parent.Children.Remove(child);
				child.Parent = null;
			}
		}
		else if(nodePort.PType == PortType.OUTPUT)
		{
			foreach(var child in nodePort.ParentNode.Children)
			{
				child.Parent = null;
			}
			nodePort.ParentNode.Children.Clear();
		}

		
		GUI.changed = true;
	}

}
