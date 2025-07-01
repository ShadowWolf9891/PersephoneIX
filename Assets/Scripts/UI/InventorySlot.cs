using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySlot
{
	public InventoryItemData item;
	public int quantity;

	public bool IsEmpty => item == null;
}
