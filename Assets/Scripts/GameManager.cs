using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    InputManager inputManager;
    ObjectiveManager objectiveManager;

    [SerializeField] private GameEvent testEvent;

	void Awake()
	{
        inputManager = GetComponent<InputManager>();
        objectiveManager = GetComponent<ObjectiveManager>();
		Cursor.lockState = CursorLockMode.Locked;
		DontDestroyOnLoad(gameObject);
	}

	// Start is called before the first frame update
	void Start()
    {
        testEvent.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
