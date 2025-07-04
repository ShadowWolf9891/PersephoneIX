using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class GameManager : MonoBehaviour
{
    InputManager inputManager;
    ObjectiveManager objectiveManager;

    [SerializeField] private GameEvent startEvent;

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
        startEvent.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseUnPauseGame()
    {
        Time.timeScale = Time.timeScale == 1.0f ? 0f : 1.0f;
	}

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}
