using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRChest : MonoBehaviour
{
    [Header("Chest Settings")]
    public Transform lid; // assign lid
    public float openAngle = 100f;
    public float openSpeed = 3f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    
    public List<GameObject> collectedItems = new List<GameObject>();
    
    private void Awake()
    {
        if (lid != null)
        {
            closedRotation = lid.localRotation;
            openRotation = Quaternion.Euler(openAngle, 0, 0) * closedRotation;
        }
    }

    private void Update()
    {
        if (lid != null)
        {
            Quaternion targetRot = isOpen ? openRotation : closedRotation;
            lid.localRotation = Quaternion.Slerp(lid.localRotation, targetRot, Time.deltaTime * openSpeed);
        }
    }

    public void OpenChest() => isOpen = true;
    public void CloseChest() => isOpen = false;
    public void ToggleChest() => isOpen = !isOpen;
    

    public void CollectItem(GameObject item)
    {
        if (!collectedItems.Contains(item))
        {
            collectedItems.Add(item);

            item.transform.SetParent(this.transform);      // parent to chest
            item.transform.localPosition = Vector3.zero;   // place at chest center
            item.transform.localRotation = Quaternion.identity; // reset rotation

            // Disable physics so it doesn’t fall
            Rigidbody rb = item.GetComponent<Rigidbody>();
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            Debug.Log("Collected: " + item.name);
        }
    }
}