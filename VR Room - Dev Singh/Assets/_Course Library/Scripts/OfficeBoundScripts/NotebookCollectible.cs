using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NotebookCollectible : MonoBehaviour
{
    [Header("Wall Target Slot")]
    public Transform wallTarget;  // Assign specific wall slot for this notebook

    private XRGrabInteractable grabInteractable;
    private bool placedInSocket = false;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void OnPlacedInSocket(SelectEnterEventArgs args)
    {
        if (placedInSocket) return; // prevent duplicates
        placedInSocket = true;

        // Disable grabbing so it doesn't get picked up again mid-move
        grabInteractable.enabled = false;

        // Move to wall target
        MoveToWall();
    }

    void MoveToWall()
    {
        if (wallTarget != null)
        {
            transform.SetParent(wallTarget.parent); // optional
            transform.position = wallTarget.position;
            transform.rotation = wallTarget.rotation;

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            Debug.Log($"{name} placed in chest, moved to wall slot.");
        }
        else
        {
            Debug.LogWarning($"No wall target assigned for {name}!");
        }
    }
}
