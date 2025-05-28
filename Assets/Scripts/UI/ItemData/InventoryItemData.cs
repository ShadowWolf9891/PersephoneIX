using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu]
public class InventoryItemData : ScriptableObject
{
    //**set up for visual UI but not implemented
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
}
