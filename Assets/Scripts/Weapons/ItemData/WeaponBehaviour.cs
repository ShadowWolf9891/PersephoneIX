using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] InputActionReference interactAction;
    [SerializeField] GameObjectEventTrigger eventToTrigger;
    [SerializeField] GameObject Weapon;

    public string GetInteractionPrompt()
    {
        throw new System.NotImplementedException();
    }

    public bool IsHoldInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteractStart()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteractStop()
    {
        throw new System.NotImplementedException();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
