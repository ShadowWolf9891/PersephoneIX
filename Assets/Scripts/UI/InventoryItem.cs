using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryItem
{
    public InventoryItemData itemData;
    public int stackSize;
    public InventoryItem(InventoryItemData item)
    {
        itemData = item;
        if (item.isStackable)
        {
            AddItem();
        }
    }
    public void AddItem()
    {
        stackSize++;
    }
    public void RemoveItem()
    {
        stackSize--;
    }
    
}
