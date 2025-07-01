using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryDisplay : MonoBehaviour
{
	public Inventory inventory;
	public GameObject itemSlotPrefab; // prefab with UIItemSlot
	public Transform gridParent; // panel with GridLayoutGroup

	private void Start()
	{
		Refresh();
	}

	public void Refresh()
	{
		foreach (Transform child in gridParent)
			Destroy(child.gameObject);

		foreach (var slot in inventory.slots)
		{
			var slotUIObj = Instantiate(itemSlotPrefab, gridParent);
			var slotUI = slotUIObj.GetComponent<UIItemSlot>();
			slotUI.Setup(slot);
		}
	}
}
