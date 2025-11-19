using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Required for UnityEvent

public class Puzzle1Manager : MonoBehaviour
{
    // Set this to 12 in the Inspector
    public int totalPieces = 12;

    // This will keep track of how many pieces are placed
    private int currentPieces = 0;

    // This is a special event you can hook things up to in the Inspector
    // Use this to trigger the image reveal, audio cues, etc.
    public UnityEvent OnPuzzleCompleted;

    // This is the public function you will call from each puzzle slot
    public void PiecePlaced()
    {
        // Add 1 to the counter
        currentPieces++;

        // Check if the puzzle is complete
        if (currentPieces >= totalPieces)
        {
            Debug.Log("Puzzle 1 Complete!");

            // Fire the completion event
            if (OnPuzzleCompleted != null)
            {
                OnPuzzleCompleted.Invoke();
            }
        }
    }
}