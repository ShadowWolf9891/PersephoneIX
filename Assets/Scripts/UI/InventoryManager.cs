using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<InventoryItemData, InventoryItem> itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
    void Awake()
    {
        if(instance == null) 
		{
			instance = this; 
		}
		else
		{
			Destroy(this);
		}
    }
    public void Add(InventoryItemData itemData)
    {
        //checks if item is currently in inventory
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            //**checks if item is stackable before adding item, might need changed
            if (itemData.isStackable) item.AddItem();
            Debug.Log($"{itemData.itemName}, {item.stackSize}");
        }
        else
        {
            //adds new inventory item to list and dictionary
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            Debug.Log($"{itemData.itemName} added");
        }
    }
    public void Remove(InventoryItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            //removes the item then checks if that was last item in stack before removing from list/dictionary
            item.RemoveItem();
            if (item.stackSize <= 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
                Debug.Log($"{itemData.itemName} removed");
            }
        }
    }
}
