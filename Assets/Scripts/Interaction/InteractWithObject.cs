using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractWithObject : MonoBehaviour
{

    IInteractable currentInteractable;

	[SerializeField] float interactRange = 2f;
	[SerializeField] LayerMask interactableLayer;
	[SerializeField] TextMeshProUGUI interactionUI;

    // Update is called once per frame
    void Update()
    {
        CheckForInteractable();
    }

    /// <summary>
    /// Raycast for prompt before interacting
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
	private void CheckForInteractable()
	{
		Ray ray = new Ray(transform.position + Vector3.up, transform.forward); // Adjust origin as needed
		if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
		{
			if (hit.collider.gameObject.TryGetComponent<IInteractable>(out var interactable))
			{
				currentInteractable = interactable;
				interactionUI.text = (currentInteractable.GetInteractionPrompt());
				return;
			}
		}

		currentInteractable = null;
		interactionUI.text = "";
	}

	public void OnInteract()
	{
		currentInteractable?.OnInteractStart();
		interactionUI.text = "";
	}


}
