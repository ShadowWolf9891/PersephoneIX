#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using System.Reflection;

using UnityEditor;
using UnityEngine;

public class GameEventCodeGenerator : EditorWindow
{
	[MenuItem("Tools/Generate Game Event Classes")]
	public static void GenerateAll()
	{
		string folderPath = EditorUtility.OpenFolderPanel("Choose Folder to Save Generated Classes", "Assets", "");

		if (string.IsNullOrEmpty(folderPath))
			return;

		if (!folderPath.StartsWith(Application.dataPath))
		{
			Debug.LogError("Path must be inside the Assets folder.");
			return;
		}

		string relativePath = "Assets" + folderPath.Substring(Application.dataPath.Length);

		var eventTypes = AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(a => a.GetTypes())
			.Where(t =>
				t.IsClass &&
				!t.IsAbstract &&
				t.BaseType != null &&
				t.BaseType.IsGenericType &&
				t.BaseType.GetGenericTypeDefinition() == typeof(GameEvent<>))
			.ToList();

		foreach (var eventType in eventTypes)
		{
			Type dataType = eventType.BaseType.GetGenericArguments()[0];
			string eventName = eventType.Name;

			// -------- Generate Listener --------
			string listenerClassName = $"{eventType.Name}Listener";
			string listenerPath = Path.Combine(relativePath, listenerClassName + ".cs");

			if (!File.Exists(listenerPath))
			{
				string listenerCode = GenerateListenerCode(listenerClassName, dataType);
				File.WriteAllText(listenerPath, listenerCode);
				Debug.Log($"Created Listener: {listenerPath}");
			}

			// -------- Generate Conditional Trigger --------
			string triggerClassName = $"Conditional{eventName}";
			string triggerPath = Path.Combine(relativePath, triggerClassName + ".cs");

			if (!File.Exists(triggerPath))
			{
				string triggerCode = GenerateTriggerCode(triggerClassName, eventType.Name, dataType);
				File.WriteAllText(triggerPath, triggerCode);
				Debug.Log($"Created Trigger: {triggerPath}");
			}
		}

		AssetDatabase.Refresh();
	}

	static string GenerateListenerCode(string className, Type dataType)
	{
		string filter = "";
		string dataTypeName = dataType.FullName;

		if (dataType == typeof(GameObject))
		{
			filter = $@"
    public override void OnEventRaised(GameObject value)
    {{
        if (value == gameObject)
        {{
            base.OnEventRaised(value);
        }}
    }}";
		}

		return $@"using UnityEngine;

[AddComponentMenu(""Events/Listeners/{className}"")]
public class {className} : GameEventListener<{dataTypeName}>
{{{filter}
}}";
	}

	static string GenerateTriggerCode(string className, string eventTypeName, Type dataType)
	{
		string dataTypeName = dataType.FullName;

		return $@"using UnityEngine;

[AddComponentMenu(""Events/Triggers/{className}"")]
public class {className} : ConditionalEventTrigger<{eventTypeName}, {dataTypeName}> {{ }}
";
	}
}
#endif