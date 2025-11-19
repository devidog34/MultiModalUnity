using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Puzzle3Manager : MonoBehaviour
{
    [Header("Puzzle Setup")]
    public List<int> correctSequence = new List<int>(); // This gets set by the NPC from Puzzle 2

    [Header("Rewards / Next Stage")]
    [Tooltip("The note/riddle that appears on the mirror")]
    public GameObject puzzle3RewardNote;

    [Tooltip("The Parent GameObject for Puzzle 4 (The Gate & NPC)")]
    public GameObject puzzle4Parent;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip successSound;
    public AudioClip failSound;

    // Internal tracking
    private List<int> playerSequence = new List<int>();
    private bool isSolved = false;

    void Start()
    {
        // Hide the reward and the next puzzle when the game starts
        if (puzzle3RewardNote) puzzle3RewardNote.SetActive(false);
        if (puzzle4Parent) puzzle4Parent.SetActive(false);

        // --- TEST CODE (Delete later) ---
        // If no sequence is set by the NPC yet, use this default one for testing
        if (correctSequence.Count == 0)
        {
            correctSequence.AddRange(new int[] { 0, 1, 2, 3 });
        }
    }

    public void OnButtonPressed(int id)
    {
        if (isSolved) return;

        Debug.Log($"Button {id} pressed!");
        playerSequence.Add(id);

        CheckSequence();
    }

    void CheckSequence()
    {
        // Check the most recent button press
        int currentIndex = playerSequence.Count - 1;

        // If the button they just pressed doesn't match the correct sequence at that spot...
        if (playerSequence[currentIndex] != correctSequence[currentIndex])
        {
            ResetPuzzle();
            return;
        }

        // If we are here, they are correct so far.
        // Did they finish the whole sequence?
        if (playerSequence.Count == correctSequence.Count)
        {
            SolvePuzzle();
        }
    }

    void SolvePuzzle()
    {
        isSolved = true;
        Debug.Log("Puzzle 3 Solved! Revealing Puzzle 4.");

        // 1. Play Sound
        if (audioSource && successSound) audioSource.PlayOneShot(successSound);

        // 2. Show the Note on the Mirror
        if (puzzle3RewardNote) puzzle3RewardNote.SetActive(true);

        // 3. REVEAL PUZZLE 4 (The Gate & NPC)
        if (puzzle4Parent) puzzle4Parent.SetActive(true);
    }

    void ResetPuzzle()
    {
        Debug.Log("Wrong order! Resetting.");
        if (audioSource && failSound) audioSource.PlayOneShot(failSound);
        playerSequence.Clear();
    }

    // Call this from your Puzzle 2 NPC script
    public void SetCorrectSequence(List<int> sequence)
    {
        correctSequence = sequence;
    }
}