using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] InputActionReference interactAction;
    public InventoryItemData itemData;
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
        //InventoryManager.instance.Add(itemData);
        Destroy(gameObject);
    }
    public void OnInteractStop()
    {

    }
}
