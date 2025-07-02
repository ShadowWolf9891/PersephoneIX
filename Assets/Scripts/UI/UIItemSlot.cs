using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
	[SerializeField] UIInventoryDisplay inventoryDisplay;
	public Image backgroundImage;
	public Image iconImage;
	public TextMeshProUGUI quantityText;

	public Sprite defaultSlotSprite; //For when there is nothing in that inventory slot

	private InventoryItemData inventoryItemData;

	public void Setup(InventorySlot slot, UIInventoryDisplay inventoryDisplay)
	{
		this.inventoryDisplay = inventoryDisplay;
		if (slot.IsEmpty)
		{
			iconImage.enabled = false;
			quantityText.text = "";
			backgroundImage.sprite = defaultSlotSprite;
		}
		else
		{
			iconImage.sprite = slot.item.icon;
			iconImage.enabled = true;
			quantityText.text = slot.item.isStackable ? slot.quantity.ToString() : "";
			backgroundImage.sprite = defaultSlotSprite;
			inventoryItemData = slot.item;
		}
		GetComponent<Button>().onClick.AddListener(() => ClickedItem());
		
	}

	public InventoryItemData GetItem()
	{
		return inventoryItemData;
	}
	public void ClickedItem()
	{
		Debug.Log($"Clicked {this.name}");
		inventoryDisplay.SetActiveSlot(this);
	}
}
