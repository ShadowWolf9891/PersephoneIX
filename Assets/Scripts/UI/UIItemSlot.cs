using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
	public Image backgroundImage;
	public Image iconImage;
	public TextMeshProUGUI quantityText;

	public Sprite defaultSlotSprite; //For when there is nothing in that inventory slot

	public void Setup(InventorySlot slot)
	{
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
		}
	}
}
