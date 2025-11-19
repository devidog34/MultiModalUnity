using System.Collections;
using System.Collections.Generic;
// PuzzleButton.cs
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// This script goes on EACH pressable button object.
// Your button object MUST also have an XRSimpleInteractable (or any XRBaseInteractable).
[RequireComponent(typeof(XRBaseInteractable))]
public class PuzzleButton : MonoBehaviour
{
    [Tooltip("The ID for this button. 0 for the first, 1 for the second, etc.")]
    public int buttonID;

    [Tooltip("Drag your 'Puzzle3_Manager' GameObject here.")]
    public Puzzle3Manager puzzleManager;

    private XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();

        if (puzzleManager == null)
        {
            Debug.LogError("Puzzle Manager is not assigned on " + gameObject.name);
        }
    }

    // Subscribe to the "select" event (which is like a "press" for this component)
    void OnEnable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnButtonPressed);
        }
    }

    void OnDisable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnButtonPressed);
        }
    }

    // This function is called when the player "selects" (presses) this object
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        // Tell the puzzle manager that this specific button was pressed
        if (puzzleManager != null)
        {
            puzzleManager.OnButtonPressed(buttonID);
        }
    }
}
