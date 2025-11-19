using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI work

public class PuzzleUIButton : MonoBehaviour
{
    [Tooltip("The ID of this button (0, 1, 2, 3, etc)")]
    public int buttonID;

    [Tooltip("Drag your Puzzle3_Manager object here")]
    public Puzzle3Manager manager;

    private Button myButton;

    void Start()
    {
        // Automatically find the Button component on this object
        myButton = GetComponent<Button>();

        if (myButton != null)
        {
            // Listen for the click event
            myButton.onClick.AddListener(OnClicked);
        }
        else
        {
            Debug.LogError("PuzzleUIButton script is on an object without a Button component!");
        }
    }

    void OnClicked()
    {
        if (manager != null)
        {
            manager.OnButtonPressed(buttonID);
        }
    }
}
