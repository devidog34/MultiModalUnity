using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollectibleItem : MonoBehaviour
{
    public XRChest chest;

    public void OnGrab(SelectEnterEventArgs args)
    {
        if (chest != null)
        {
            XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

            if (grabInteractable != null)
            {
                // Disable the grab interactable to release it from the hand
                grabInteractable.enabled = false;
            }

            // Now safely move the object into the chest
            chest.CollectItem(gameObject);
        }
    }
}