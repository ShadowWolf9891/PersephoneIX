using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Inventory/Item")]
public class InventoryItemData : ScriptableObject
{
    //**set up for visual UI but not implemented
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
    public bool isStackable;
    public int MaxStack = 1;
}
