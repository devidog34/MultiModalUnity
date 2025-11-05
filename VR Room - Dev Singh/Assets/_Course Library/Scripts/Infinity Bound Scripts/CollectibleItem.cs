using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollectibleItem : MonoBehaviour
{
    public XRChest chest;

    // This will be called by On Select Entered
    public void OnGrab(SelectEnterEventArgs args)
    {
        if (chest != null)
        {
            chest.CollectItem(gameObject);
        }
    }
}