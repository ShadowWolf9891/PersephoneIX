using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDisplay : MonoBehaviour
{
	public Inventory inventory;
	[SerializeField] GameObject itemSlotPrefab; // prefab with UIItemSlot
	[SerializeField] Transform gridParent; // panel with GridLayoutGroup
	[SerializeField] GameObject PlayerHUD;
	
	[SerializeField] GameEvent toggleMovementEvent;

	[SerializeField] TextMeshProUGUI nameBox;
	[SerializeField] TextMeshProUGUI descriptionBox;
	[SerializeField] Image imageBox; 

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
			slotUI.Setup(slot, this);
		}
	}

	public void ToggleInventory()
	{
		if(gridParent.parent.gameObject.activeInHierarchy)
		{
			gridParent.parent.gameObject.SetActive(false);
			PlayerHUD.SetActive(true);
			Cursor.lockState = CursorLockMode.Locked;
			
		}
		else
		{
			gridParent.parent.gameObject.SetActive(true);
			PlayerHUD.SetActive(false);
			Cursor.lockState = CursorLockMode.Confined;
			Refresh();
		}
		
		toggleMovementEvent.Raise();
	}

	public void SetActiveSlot(UIItemSlot activeSlot)
	{
		if (activeSlot.GetItem() != null)
		{
			nameBox.text = activeSlot.GetItem().itemName;
			descriptionBox.text = activeSlot.GetItem().description;
			imageBox.sprite = activeSlot.GetItem().icon;
		}
		else
		{
			nameBox.text = "";
			descriptionBox.text = "";
			imageBox.sprite = activeSlot.defaultSlotSprite;
		}
		
	}
}
