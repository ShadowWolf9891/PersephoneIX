using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] InputActionReference interactAction;
    [SerializeField] InventoryItemData itemData;

    private Inventory playerInventory;
	public void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    public string GetInteractionPrompt() //"Press 'E' to open"
    {
        return $"Press {interactAction.action.GetBindingDisplayString()} \n {itemData.itemName}";
    }

    public bool IsHoldInteraction() //Should the button be held down to interact
    {
        return false;
    }
    public void OnInteractStart() //When the button is first held down
    {
        Debug.Log("Interacted with Item");
        playerInventory.Add(itemData);
        Destroy(gameObject);
    }
    public void OnInteractStop()
    {

    }
}
