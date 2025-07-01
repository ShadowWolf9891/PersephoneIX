using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Removed singleton instance to allow for chests / npc inventory etc.

    public int width = 8; //Number of slots
    public int height = 6;
    public List<InventorySlot> slots = new List<InventorySlot>();

    void Awake()
    {
		for (int i = 0; i < width * height; i++)
			slots.Add(new InventorySlot());
	}
    public bool Add(InventoryItemData itemData, int amount = 1)
    {
		for (int i = 0; i < slots.Count; i++)
		{
			if (!slots[i].IsEmpty && slots[i].item == itemData && itemData.isStackable)
			{
                if (slots[i].quantity + amount < itemData.MaxStack) //Existing stack has enough room 
                {
                    slots[i].quantity += amount;
                    return true;
                }
                else if(slots[i].quantity < itemData.MaxStack) //Stack becomes full before adding all items
                {
                    amount -= itemData.MaxStack - slots[i].quantity;
					slots[i].quantity = itemData.MaxStack;
                    //Don't return here, do logic for creating a new stack.
                }
			}
		}

		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i].IsEmpty) //Get first empty slot
			{
                if (amount <= itemData.MaxStack)
                {
                    slots[i].item = itemData;
                    slots[i].quantity = amount;
                    return true;
                }
                else
                {
					slots[i].item = itemData;
					slots[i].quantity = itemData.MaxStack;
                    amount -= itemData.MaxStack;
                    //Don't return and get the next empty slot
				}
				
			}
		}

		return false; // Inventory full
    }
    public bool Remove(InventoryItemData itemData, int amount = 1)
    {
		for (int i = 0; i < slots.Count; i++)
		{
			if (!slots[i].IsEmpty && slots[i].item == itemData && itemData.isStackable)
			{
				if (slots[i].quantity - amount < 0) //if you cannot remove that amount from the stack
				{
					//NOTE: This does not factor multiple slots of the same item into account.
					return false;
				}
				else
				{
					//Remove item
					slots[i].quantity -= amount;
					if (slots[i].quantity <= 0) //If there is none of the item left, remove it completly.
					{
						slots[i].item = null;
						slots[i].quantity = 0;
					}
					return true;	
				}
			}
		}
		return false; //Failed to remove item
	}
}
