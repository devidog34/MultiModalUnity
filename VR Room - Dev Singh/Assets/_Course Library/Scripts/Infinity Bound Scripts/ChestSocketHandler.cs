using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChestSocketHandler : MonoBehaviour
{
    [Header("Socket Reference")]
    public XRSocketInteractor socket;

    [Header("Wall Target Points (in order)")]
    public Transform[] wallTargets;

    [Header("Movement Settings")]
    public float moveDuration = 1.5f;    // How long it takes to move to the wall
    public float scaleMultiplier = 2f;   // How much bigger the object gets

    private int nextTargetIndex = 0;

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnItemPlaced);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnItemPlaced);
    }

    private void OnItemPlaced(SelectEnterEventArgs args)
    {
        GameObject placedObject = args.interactableObject.transform.gameObject;

        if (nextTargetIndex >= wallTargets.Length)
        {
            Debug.Log("All wall targets filled!");
            return;
        }

        Transform target = wallTargets[nextTargetIndex];
        nextTargetIndex++;

        Debug.Log($"ChestSocketHandler: Moving {placedObject.name} to {target.name}");

        // Temporarily disable socket to release the object
        socket.enabled = false;

        // Disable physics so the object can move freely
        Rigidbody rb = placedObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // Start moving & scaling
        StartCoroutine(MoveAndScaleRoutine(placedObject.transform, target));

        // Re-enable socket after a short delay
        StartCoroutine(ReenableSocketAfterDelay(0.1f));
    }

    private IEnumerator MoveAndScaleRoutine(Transform obj, Transform wallTarget)
    {
        Vector3 startPos = obj.position;
        Quaternion startRot = obj.rotation;
        Vector3 startScale = obj.localScale;
        Vector3 targetScale = startScale * scaleMultiplier;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;

            obj.position = Vector3.Lerp(startPos, wallTarget.position, t);
            obj.rotation = Quaternion.Lerp(startRot, wallTarget.rotation, t);
            obj.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        // Ensure final position, rotation, and scale are exact
        obj.position = wallTarget.position;
        obj.rotation = wallTarget.rotation;
        obj.localScale = targetScale;
    }

    private IEnumerator ReenableSocketAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        socket.enabled = true;
    }
}